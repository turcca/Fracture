Shader "Custom/ValveFlow" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_FlowTex ("Flow", 2D) = "white" {}
		_NoiseTex ("Noise", 2D) = "black" {}
		_WaveTex ("Wave", 2D) = "white" {}
		_WaveTex2 ("Wave2", 2D) = "white" {}
		_ColorRamp ("Ramp", 2D) = "white" {}
		_TexScale ("Scale", Float) = 1.0
		_Speed ("Speed", Float) = 0.5
		//_DispTex ("Displace (RGB)", 2D) = "white" {}
		//_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert fullforwardshadows alpha
		#pragma debug

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
 		//#include "UnityCG.cginc"

		sampler2D _MainTex;
		sampler2D _FlowTex;
		sampler2D _NoiseTex;
		sampler2D _WaveTex;
		sampler2D _WaveTex2;
		sampler2D _ColorRamp;
		float _TexScale;
		float _Speed;
		//sampler2D _DispTex;

		struct Input {
			float2 uv_MainTex;
			//float2 uv_NoiseTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			float _FlowMapOffset0 = frac(_Time.x*_Speed);
			float _FlowMapOffset1 = frac(_Time.x*_Speed+0.5);
			float _HalfCycle = 0.5;
			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			
			//get and uncompress the flow vector for this pixel
			float2 samplemap = tex2D(_FlowTex, IN.uv_MainTex).rg * 2.0f - 1.0f;
			float2 flowmap;
			flowmap.x = -samplemap.x;
			flowmap.y = samplemap.y;
			
			//float cycleOffset = tex2D(_NoiseTex, IN.uv_MainTex).r*0.2;
			float cycleOffset = 1.0f;
			
			float phase0 = cycleOffset * _FlowMapOffset0;
			float phase1 = cycleOffset * _FlowMapOffset1;
			//loat phase0 = _FlowMapOffset0;
			//float phase1 = _FlowMapOffset1;

			// Sample color
			float3 colorT0 = tex2D(_MainTex, (IN.uv_MainTex * _TexScale) + flowmap * phase0 );
			float3 colorT1 = tex2D(_MainTex, (IN.uv_MainTex * _TexScale) + flowmap * phase1 );
			float colorLerp = ( abs(_HalfCycle - _FlowMapOffset0 ) / _HalfCycle );
			float3 col = lerp(colorT0, colorT1, colorLerp);

			// Sample normal map.
			float3 normalT0 = tex2D(_WaveTex, (IN.uv_MainTex * _TexScale) + flowmap * phase0 );
			float3 normalT1 = tex2D(_WaveTex2, (IN.uv_MainTex * _TexScale) + flowmap * phase1 );
			float flowLerp = ( abs(_HalfCycle - _FlowMapOffset0 ) / _HalfCycle );
			//float3 normalOffset = lerp( normalT0, normalT1, flowLerp );
			float3 normalOffset = (1.0-flowLerp)*normalT0 + (flowLerp)*normalT1;
			
			//col = tex2D(_MainTex, IN.uv_MainTex);
			//float3 ramp = tex2D(_ColorRamp, float2(length(flowmap),0));
			//o.Albedo = ramp;
			o.Albedo = float3(1.0, 1.0, 1.0);
			o.Alpha = 1;
			//o.Albedo = fixed3(1,1,1);
			//o.Alpha = abs(flowmap.r)+abs(flowmap.g);
			
			normalOffset = normalOffset*2.0-1.0;
			float3 calculatedNormal = float3(normalOffset.xy,
				sqrt(1.0-normalOffset.x*normalOffset.x - normalOffset.y*normalOffset.y));
			//float3 calculatedNormal = float3(0.0, 0.0, 1.0);
			o.Normal = calculatedNormal;
			//o.Albedo = fixed3(1,1,1);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
