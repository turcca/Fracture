Shader "Custom/FractureFog" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NoiseTex ("Noise", 2D) = "white" {}
		_DensityTex ("DensityField", 2D) = "black" {}
		_NormalTex ("Normals", 2D) = "white" {}
		_WorldPosition ("WorldPos", Vector) = (0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NoiseTex;
		sampler2D _DensityTex;
		sampler2D _NormalTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		fixed3 _WorldPosition;
		
		#define MAX_STEPS 50
		#define MAX_DISTANCE 100.0f
		
		float get_density(float2 p)
		{
			float2 world_coord = p*200.0f-100.0f + _WorldPosition.xz;
			float2 density_map_coord = (world_coord + float2(500.0f, 250.0f)) / float2(1000.0f, 500.0f);
			float density_map = (tex2D(_DensityTex, density_map_coord.xy).r) * 0.5f + 0.1f;
			
			float dens = density_map * 20.0f;
			float rand = tex2D(_NoiseTex, p.xy+_WorldPosition.xz/200.0f).r * 2.0f;
			float rand2 = tex2D(_NoiseTex, p.xy*10.0f);
			return dens + rand + rand2;
		}
		
		float turbulence(float2 p)
		{
			//float rand2 = tex2D(_NoiseTex, p.xy *3.0f + _Time.x)*0.1f;
			return 0.0f;		
		}
		
		float ray_march(float2 origin, float2 target)
		{
			float max_distance = length(target - origin);
			float2 dir = normalize(target - origin);
			float delta = max_distance / MAX_STEPS;
			float d = 0.0;
			float c = 0.0;
			
  			for (int i = 0; i < MAX_STEPS; i++) {
    			float2 new_point = origin + dir * d;
    			float new_color = get_density(new_point)* delta;
    			
    			c += new_color;    			
    			if (c > 1.0f) break;
    			
    			d += delta;
  			}
  			c += turbulence(target);
  			return clamp(c, 0.0f, 1.0f);
		}

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			float2 world_coord = IN.uv_MainTex*200.0f-100.0f + _WorldPosition.xz;
			float2 density_map_coord = (world_coord + float2(500.0f, 250.0f)) / float2(1000.0f, 500.0f);
			fixed4 c = tex2D (_MainTex, density_map_coord.xy) * _Color;
			o.Albedo = c.rgb;
			
			float2 origin = float2(0.5f, 0.5f);
			float2 target = IN.uv_MainTex;
			float alpha = ray_march(origin, target);
			alpha = alpha * alpha * alpha;
						

			float x = target.x*2.0f-1.0f;
			float y = target.y*2.0f-1.0f;
			o.Normal = normalize(float3(x,y, 1.0 - alpha));
			
			//float3 normalOffset = tex2D(_NormalTex, (IN.uv_MainTex + _WorldPosition.xz/200.0f) * 5.0f);
			//normalOffset = normalOffset*2.0-1.0;
			//float3 calculatedNormal = float3(normalOffset.xy,
			//	sqrt(1.0-normalOffset.x*normalOffset.x - normalOffset.y*normalOffset.y));
				
			//o.Normal = o.Normal * 0.2f + calculatedNormal * 0.8f;
		
			//o.Normal = normalize(o.Normal);
			
			o.Alpha = alpha;
			//o.Albedo.r = alpha;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
