// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Camera Shader/MuteColors"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Desaturation("Desaturation", Range(-1.0,1.0)) = 0.5
		_Darken("DarkenFactor", Range(-1.0,1.0)) = 0.5
		_RedChannel("RedChannel", Range(0.0,2.0)) = 0.5
		_GreenChannel("GreenChannel", Range(0.0,2.0)) = 0.85
		_BlueChannel("BlueChannel", Range(0.0,2.0)) = 1.5
		_ContrastFactor("ContrastFactor", Range(0.0,10.0)) = 1.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed _Desaturation;
			fixed _Darken;
			fixed _RedChannel;
			fixed _GreenChannel;
			fixed _BlueChannel;
			fixed _ContrastFactor;


			//Get medium color value
			fixed GetMCV(fixed3 col)
			{
				return (col.r + col.g + col.b) / 3;
			}


			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				fixed cMP = GetMCV(col.rgb);

				col.rgb += ((cMP - col.rgb) * _Desaturation);
				col.rgb -= _Darken;

				col.r *= _RedChannel ;
				col.g *= _GreenChannel;
				col.b *= _BlueChannel ;
				col.rgb = (((col.rgb - 0.5f) * _ContrastFactor) + 0.5f);

				return col;
			}
			ENDCG
		}
	}
}
