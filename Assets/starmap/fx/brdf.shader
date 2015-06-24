Shader "Custom/brdf"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("BRDF", 2D) = "gray" {}
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
		sampler2D _NormalMap;
		fixed4 _SpecularColor;
		float _Shininess;
		
		struct Input
		{
			float2 uv_MainTex;
			float3 viewDir;
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
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			//half4 c = float4(0.5, 0.5, 0.5, 1.0);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			if (rim > 0.65f)
			{
				o.Albedo = float3(0.3f, 0.3f, 1.0f);
				o.Emission = float3(0.3f, 0.3f, 1.0f);
			}
          	//o.Emission = float3(0.0f, 0.0f, 1.0f) * pow (rim, 1.0);
			//o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_MainTex));
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
