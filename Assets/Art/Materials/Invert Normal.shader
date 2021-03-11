Shader "Special/Inverted Diffuse" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Front (RGB)", 2D) = "white" {}
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200
		Cull Front
		CGPROGRAM
#pragma surface surf Lambert


		sampler2D _MainTex;
	fixed4 _Color;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		o.Normal = -o.Normal;
		fixed4 c = tex2D(_MainTex, float2(1 - IN.uv_MainTex.x,IN.uv_MainTex.y)) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	}

		Fallback "VertexLit"
}
