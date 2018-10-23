Shader "ViveSR/Standard, Stencil" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "bump" {}
		_Emission("Emission", Range(0, 1)) = 0
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_MetallicGlossMap("Metallic", 2D) = "white"
		[Enum(UnityEngine.Rendering.CullMode)]_CullMode("Culling", int) = 2						// def: back
		_StencilValue ("StencilRefValue", float) = 1		
		[Enum(UnityEngine.Rendering.CompareFunction)]_StencilComp("Stencil Compare", int) = 0	// disable
		[Enum(UnityEngine.Rendering.CompareFunction)]_ZTestComp("ZTest Compare", int) = 4		// lequal
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Cull [_CullMode]
		ZTest[_ZTestComp]
		Stencil{
			Ref [_StencilValue]
			Comp[_StencilComp]
		}

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
		#pragma multi_compile __  CLIP_PLANE
		#include "../ViveSRCG.cginc"

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_MetallicGlossMap;
			WORLD_POS_FORCLIP_SURF
		};

		half _Glossiness;
		half _Emission;
		fixed4 _Color;
		sampler2D _BumpMap;
		sampler2D _MetallicGlossMap;
		DECLARE_CLIP_PLANE_VARIABLE

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			CLIP_PLANE_TEST(IN)

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 m = tex2D(_MetallicGlossMap, IN.uv_MetallicGlossMap) * _Glossiness;
			o.Albedo = c.rgb;

			// Metallic and smoothness come from slider variables
			o.Metallic = m.rgb;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Emission = c.rgb * _Emission;			
		}
		ENDCG
	}
	FallBack "Diffuse"
}
