// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BlackNWhiteAura" 
{
	Properties
	{
		_Difraction("difraction", Float) = 0.0
	}

	Category
	{
		// We must be transparent, so other objects are drawn before this one.
		Tags{ "Queue" = "Transparent" "RenderType" = "Opaque" }

		SubShader
		{
			// This pass grabs the screen behind the object into a texture.
			// We can access the result in the next pass as _GrabTexture
			GrabPass
			{
				Name "BASE"
				Tags{ "LightMode" = "Always" }
			}

			// Main pass: Take the texture grabbed above and use the bumpmap to perturb it
			// on to the screen
			Pass
			{
				Name "BASE"
				Tags{ "LightMode" = "Always" }

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex : POSITION;
					float2 texcoord: TEXCOORD0;
					float3 viewDir : TEXCOORD1;
					float3 normal : NORMAL;
				};

				struct v2f
				{
					float4 vertex : SV_POSITION;
					float4 uvgrab : TEXCOORD0;
					float3 viewDir : TEXCOORD1;
					float4 screenPos : TEXCOORD2;
					float3 normal : NORMAL;
					UNITY_FOG_COORDS(3)
				};

				float _Difraction;

				v2f vert(appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);

					#if UNITY_UV_STARTS_AT_TOP
						float scale = -1.0;
					#else
						float scale = 1.0;
					#endif

					o.screenPos = ComputeScreenPos(o.vertex);
					o.viewDir = WorldSpaceViewDir(v.vertex);
					o.uvgrab.xy = o.vertex.xy*scale*0.5;//o.screenPos.xy;//(float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;

														//o.uvgrab.xy = o.vertex.xy * scale *0.5;
					o.uvgrab.zw = o.vertex.zw;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}
				sampler2D _GrabTexture;
				float4 _GrabTexture_TexelSize;

				half4 frag(v2f i) : SV_Target
				{
					i.uvgrab.xy = i.uvgrab.z + i.uvgrab.xy;

					half4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
					half finalCol = (col.x + col.y + col.z) / 3;

					half rim = 1.0 - saturate(dot(normalize(i.viewDir), i.normal));
					col.rgb = i.normal;//half4(finalCol, finalCol, finalCol, col.a);
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG
			}
		}
		SubShader
		{
			Blend DstColor Zero
			Pass
			{
				Name "BASE"
				SetTexture[_MainTex]{ combine texture }
			}
		}
	}
}
