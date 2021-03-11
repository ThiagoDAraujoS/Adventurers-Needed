﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced '_ProjectorClip' with 'unity_ProjectorClip'

Shader "Projector/Magic" {
	Properties{
		_ShadowTex("Cookie", 2D) = "" {}
	_Intensity("Intensity", Range(0.0,2.0)) = 0.0
	}

		Subshader{
		Tags{ "Queue" = "Transparent" }
		Pass{
		ZWrite Off
		ColorMask RGB
		Blend One One
		Offset - 1, -1

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fog
#include "UnityCG.cginc"

	struct v2f {
		float4 uvShadow : TEXCOORD0;
		float4 uvFalloff : TEXCOORD1;
		UNITY_FOG_COORDS(2)
			float4 pos : SV_POSITION;
	};

	float4x4 unity_Projector;
	float4x4 unity_ProjectorClip;

	v2f vert(float4 vertex : POSITION)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(vertex);
		o.uvShadow = mul(unity_Projector, vertex);
		o.uvFalloff = mul(unity_ProjectorClip, vertex);
		UNITY_TRANSFER_FOG(o,o.pos);
		return o;
	}

	fixed _Intensity;
	sampler2D _ShadowTex;

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 texS = tex2Dproj(_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
	texS.a = 1.0 - texS.a;

	fixed4 res = (texS * ((sin(_Time.w)*0.25 + 0.7)))  * _Intensity;

	UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(0,0,0,0));
	return res;
	}
		ENDCG
	}
	}
}
