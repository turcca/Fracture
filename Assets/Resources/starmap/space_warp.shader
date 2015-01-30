Shader "Custom/space_warp"
{
Properties
{
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_FlowTex ("Flow (RGB)", 2D) = "white" {}
}
SubShader
{
	Tags { "RenderType"="Opaque" }
	LOD 200
        Lighting off
		
	CGPROGRAM
	#pragma surface surf Lambert

	sampler2D _FlowTex;
	sampler2D _MainTex;

	struct Input
	{
		float2 uv_MainTex;
	};
	
	float rand(half2 co)
	{
    		return fract(sin(dot(co.xy, half2(12.9898,78.233))) * 43758.5453);
	}

	void surf (Input IN, inout SurfaceOutput o)
	{
		//half4 flow = tex2D (_FlowTex, IN.uv_MainTex);
		//half2 pos;
		//pos.x = (flow.r -0.5f) * 0.1f * (_SinTime.y);
		//pos.y = (flow.g -0.5f) * 0.1f * (_CosTime.y);
		//half4 c = tex2D (_MainTex, IN.uv_MainTex + pos.xy);
		
		half4 c;
		c.r = rand(half2(IN.uv_MainTex.x, IN.uv_MainTex.y));
		o.Albedo = c.rgb;
		o.Alpha = c.a;
		o.Emission = c.rgb;
	}
	ENDCG
} 
FallBack "Diffuse"
}
