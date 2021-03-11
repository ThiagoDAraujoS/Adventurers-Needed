// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "SnowBlit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BlitTex("Decalc", 2D) = "white"{}
		_X ("Center X", Float) = 0.0
		_Y ("Center Y", Float) = 0.0
		_SizeX("Size X", Float) = 0.0
		_SizeY("Size Y", Float) = 0.0
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
			sampler2D _BlitTex;
			float _X;
			float _Y;
			float _SizeX;
			float _SizeY;
			fixed4 frag (v2f i) : SV_Target
			{
				//use texel to put the smaller texture at the right place
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 col3 = tex2D(_BlitTex, i.uv);

				return col * col3;
			}
			ENDCG
		}
	}
}
