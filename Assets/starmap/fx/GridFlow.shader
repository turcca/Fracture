
//
// Fragment shader, Tiled Directional Flow
//
// (c) 2010 frans van hoesel, university of groningen
//
//
// this shader creates animated water by transforming normalmaps
// the scaling and rotation of the normalmaps is done per tile
// and is constant per tile. Each tile can have its own parameters
// for rotation, scaling, and speed of translation
// To hide the seams between the tiles, all seams have another tile
// centered over the seam. The opacity of the tiles decreases towards the
// edge of the tiles, so the edge isn't visible at all.
// Basically, all points have four tiles (A,B,C and D), mixed together
// (although at the edges the contribution of a tile to this mix is 
// reduced to zero).
// The mixing of the tiles each with different parameters gives a nice
// animated look to the water. It is no longer just sliding in one direction, but
// appears to move more like real water. 

// The resulting sum of normalmaps, is used to calculate the refraction of the clouds 
// in a cube map and can also be used for other nice effects. In this example the 
// colormap of the material under water is distorted to fake some kind of refraction
// (for this example the water is a bit too transparent, but it shows this refraction
// better) 

// A flowmap determines in the red and green channel the normalized direction of the
// flow and in the blue channel wavelength.
// The alpha channel is used for the transparency of the water. Near the edge, the 
// water becomes less deep and more transparent. Also near the edge, the waves tend
// to be smaller, so the same alpha channel also scales the height of the waves.
// Currently the wavelength is in its own channel (blue), but could be premultiplied
// to the red and green channels. This makes this channel available for changing the 
// speed of the waves per tile.


// Further improvements
// Besides the obvious improvements mentioned in the code (such as premultiplying
// the direction of the waves with the scale, or moving the texscale multiplication
// to the texture coordinates), one could get rid of tiling in this code and pass it 
// tiled geometry. This way the whole lookup of the flowmap (which is constant over 
// each tile) could be moved to the vertexshader, removing the the construction of 
// the flow rotation matrix. As this is done 4 times per pixel, it might give a big 
// performance boost (one does need to pass on 4 constant matrices to the fragment
// shader, which will cost you a bit of performance).
// 
//////////////////////////////////////////////////////////////////////////////////
//                     This software is Creditware:
//
// you can do whatever you want with this shader except claiming rights 
// you may sell it, but you cannot prevent others from selling it, giving it away 
// or use it as they please.
// 
// Having said that, it would be nice if you gave me some credit for it, when you
// use it.
//
//                     Frans van Hoesel, (c) 2010
//////////////////////////////////////////////////////////////////////////////////


// movie at youtube: http://www.youtube.com/watch?v=TeSuNYvXAiA?hd=1 (in Germany this is blocked by youtube)
// making of at http://www.youtube.com/watch?v=wdcvPegJ1lw&hd=1 (works even in Germany)

// Thanks to Bart Campman, Pjotr Svetachov and Martijn Kragtwijk for their help.

Shader "Custom/GridFlow" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_FlowMap ("Flow", 2D) = "red" {}
		_WaterNormalMap("Water normal", 2D) = "blue" {}
		_SkyBox("SkyBox", CUBE) = "" {}
		_FlowSpeed("Flow speed", float) = 1.0
		_FlowTileScale("Flow tile scale", float) = 35.0
		_NormalTileScale("Normal tile scale", float) = 10.0
	}
	SubShader 
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert
		#pragma target 3.0

		sampler2D _MainTex, _FlowMap, _WaterNormalMap;
		samplerCUBE _SkyBox;
		float _FlowSpeed, _FlowTileScale, _NormalTileScale;

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_FlowMap;
			float2 uv_WaterNormalMap;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
		
			// texScale determines the amount of tiles generated.
			float texScale = _FlowTileScale;
			// texScale2 determines the repeat of the water texture (the normalmap) itself
			float texScale2 = _NormalTileScale;
			float myangle;
			float transp;
			float3 myNormal;
			
			float2 mytexFlowCoord = IN.uv_FlowMap * texScale;
			float2 Tcoord = IN.uv_WaterNormalMap * texScale2;
			
			// offset makes the water move
			float2 _offset = float2(_Time.x * _FlowSpeed,0);
			
			// I scale the texFlowCoord and floor the value to create the tiling
		    // This could have be replace by an extremely lo-res texture lookup
		    // using NEAREST pixel.
		    float3 flow = tex2D(_FlowMap, floor(mytexFlowCoord)/ texScale).rgb;
		    float2 speed = flow.xy*2.0 -0.5;
		    
		    // flowdir is supposed to go from -1 to 1 and the line below
		    // used to be sample.xy * 2.0 - 1.0, but saves a multiply by
		    // moving this factor two to the sample.b
		    float2 flowdir = normalize(flow.xy - 0.5);
		    // build the rotation matrix that scales and rotates the complete tile
		    float2x2 rotmat = float2x2(flowdir.x, -flowdir.y, flowdir.y ,flowdir.x);
		    // this is the normal for tile A
		    float2 NormalT0 = tex2D(_WaterNormalMap, mul(rotmat, Tcoord) - _offset).rg;
		    
			float2 NormalT = NormalT0 * 0.8f;
			// to make the water more transparent 
			//transp = tex2D( _FlowMap, IN.uv_FlowMap ).a;
			// and scale the normals with the transparency
			//NormalT *= transp*transp;
			
			// assume normal of plane is 0,0,1 and produce the normalized sum of adding NormalT to it
			myNormal = float3(NormalT, sqrt(1.0-NormalT.x*NormalT.x - NormalT.y*NormalT.y));
			o.Normal = myNormal;
			
			speed = speed / 4.0f;
			//float3 reflectDir = reflect(IN.viewDir, myNormal);
			//float3 envColor = texCUBE(_SkyBox, reflectDir).rgb;

			// very ugly version of fresnel effect
			// but it gives a nice transparent water, but not too transparent
			//myangle = dot(myNormal,normalize(IN.viewDir));
			//myangle = 0.95-0.6*myangle*myangle;
			
			// blend in the color of the plane below the water	
			
			// add in a little distortion of the colormap for the effect of a refracted
			// view of the image below the surface. 
			// (this isn't really tested, just a last minute addition
			// and perhaps should be coded differently
			
			// the correct way, would be to use the refract routine, use the alpha channel for depth of 
			// the water (and make the water disappear when depth = 0), add some watercolor to the colormap
			// depending on the depth, and use the calculated refractdir and the depth to find the right
			// pixel in the colormap.... who knows, something for the next version
			//float4 base = tex2D(_MainTex, IN.uv_MainTex + myNormal.xy/texScale2*0.03);
			float4 base = tex2D(_MainTex, IN.uv_MainTex);
			//speed = length(tex2D(_FlowMap, mytexFlowCoord/ texScale).rg);
			//float4 base = float4(1.0, 1.0, 1.0, 1.0);
			//base.r = speed;
			//base.g = speed;
			//base.b = speed;
			
			//base = float4(lerp(base.rgb,envColor,myangle*transp),1.0 );
			
			// note that smaller waves appear to move slower than bigger waves
			// one could use the tiles and give each tile a different speed if that
			// is what you want 

			o.Albedo = base.rgb;
			o.Emission = base.rgb*0.1f;
			o.Alpha = 1.0f;
			
			float x = fmod(Tcoord.x*texScale*0.5f, 1.0f);
			if (x < 0.01f || x > 0.98f)
			{
				o.Albedo += float3(0.2f,0.2f,0.3f);
			}

			float y = fmod(Tcoord.y*texScale*0.25f, 1.0f);
			if (y < 0.01f || y > 0.98f)
			{
				o.Albedo += float3(0.2f,0.2f,0.3f);
			}
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
