Shader "Custom/cs_transparent" {
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_EmissionColor("Color", Color) = (0,0,0)
		_EmissionMap("Emission", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM

#pragma surface surf Standard fullforwardshadows alpha:fade
#pragma target 3.0

		sampler2D _MainTex;
	sampler2D_float _CameraDepthTexture;

	struct Input {
		float2 uv_MainTex;
		float4 screenPos;
	};

	half _Glossiness;
	half _Metallic;
	fixed4 _Color;

	void surf(Input IN, inout SurfaceOutputStandard o)
	{
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
		o.Emission = Linear01Depth(tex2D(_CameraDepthTexture, IN.screenPos.xy / IN.screenPos.w).r);
		o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Standard"

}
