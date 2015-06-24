Shader "Custom/planet_clouds_lights"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("BRDF", 2D) = "gray" {}
		_Clouds ("Clouds", 2D) = "black" {}
		_Lights ("Lights", 2D) = "black" {}
		//_NormalMap ("Normal", 2D) = "bump" {}
		_SpecularColor("Speculat color", Color) = (0,0,0,0)
		_Shininess("Shininess", Float) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		//#pragma surface surf Lambert		
		#pragma surface surf Ramp
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Ramp;
		sampler2D _Clouds;
		sampler2D _Lights;
		sampler2D _NormalMap;
		fixed4 _SpecularColor;
		float _Shininess;
		
		struct Input
		{
			float2 uv_MainTex;
			float2 uv_Clouds;
		};
		
		half4 LightingRamp (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			float NdotL = dot(s.Normal, lightDir);
			float NdotV = dot(s.Normal, viewDir);
			
			//float diff = (NdotL * 0.5) + 0.5;
			float diff = NdotL;
			float2 uv = float2(NdotV, diff);
			float3 brdf = tex2D(_Ramp, uv.xy).rgb;
			
			float3 spec = float3(0.0,0.0,0.0);
			//if (NdotL > 0)
			//{
			//	float3 ref = reflect(-lightDir, s.Normal);
			//	float4 RdotV = pow(max(0.0, dot(ref, viewDir)), _Shininess);
			//	spec = atten * _SpecularColor.rgb * RdotV;
			//}
			
			float4 c;
			c.rgb = s.Albedo * (brdf + spec);
			c.a = s.Alpha;
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 p = tex2D (_MainTex, IN.uv_MainTex);
			half4 c = tex2D (_Clouds, IN.uv_Clouds);
			half4 l = tex2D (_Lights, IN.uv_MainTex);
			o.Albedo = p.rgb* (1.0 - c.a) + c.rgb * (c.a-0.1);
			o.Emission = l.rgb * (1.0-c.a);
			o.Alpha = p.a;
			//o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_MainTex));
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
