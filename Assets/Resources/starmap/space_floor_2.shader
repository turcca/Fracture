Shader "Custom/space_floor_2" {
    Properties 
    {
        _BaseTex ("Base", 2D) = "white" {}
        _Noise ("Noise", 2D) = "black" {}
    }
 
    SubShader 
    {
        //Tags { "RenderType"="Opaque" }
        Tags { "Queue"="Transparent" }
        //Cull Off
        Blend One One
        LOD 300
 
        CGPROGRAM
        #pragma surface surf Lambert vertex:disp nolightmap
        //#pragma target 3.0
        #pragma glsl
 
        sampler2D _BaseTex;
        sampler2D _Noise;

        struct Input 
        {
            float2 uv_BaseTex;
            float2 uv_Noise;
        };
 
        void disp (inout appdata_full v)
        {
            //v.vertex.y += 0.5f* sin(0.2f* v.vertex.x + _Time.y) + 0.5f* cos(0.2f* v.vertex.z + _Time.y);4
            v.vertex.y = 0;
        }
 
        void surf (Input IN, inout SurfaceOutput o) 
        {
            float2 scroll = float2(0.3f* cos(IN.uv_Noise.x*11.0f + _Time.y/10.0f + 0.5f), 0.2f* sin(IN.uv_Noise.y*10.0f + _Time.y/10.0f + 0.2f));
            float2 scroll2 = float2(0.2f* sin(IN.uv_Noise.x*30.0f + _Time.y/10.0f), 0.2f* cos(IN.uv_Noise.y*40.0f + _Time.y/10.0f));
            //float4 off = tex2D (_Noise, IN.uv_Noise + _Time.y/100.0f);
            float4 off = float4(0,0,0,0);
            half4 c = tex2D (_BaseTex, IN.uv_BaseTex + scroll + off.xy/10.0f);
            half4 c2 = tex2D (_BaseTex, IN.uv_BaseTex/5.0f + scroll2 + off.xy/100.0f);
            //o.Albedo = c.rgb;
            o.Emission = 0.5f* c.rgb + 0.5f* c2.rgb - float3(0.2f, 0.2f, 0.2f);
            //o.Alpha = 0.333f * c.r + 0.333f * c.g + 0.333f * c.b - 0.2f;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
