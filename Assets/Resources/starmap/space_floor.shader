Shader "Custom/space_floor" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
        _Noise ("Noise", 2D) = "black" {}
	}
       SubShader {
            Pass {

    CGPROGRAM

    #pragma vertex vert
    #pragma fragment frag
    #include "UnityCG.cginc"

    struct v2f {
        float4 pos : SV_POSITION;
        float3 color : COLOR0;
    };

    v2f vert (appdata_base v)
    {
        v2f o;

        float4 p = v.vertex;
        v.vertex.y += 0.2f* sin(v.vertex.x/5.0f + _Time.y*0.5f) + 0.2f * cos (v.vertex.z/5.0f + _Time.y*0.3f);

        //v.vertex.y += clamp(p.y-2.0f,0.0f, 1.0f) * sin(v.vertex.x/0.5f + _Time.y*0.5f) + clamp(p.y-2.0f, 0.0f, 1.0f) * cos (v.vertex.z/0.5f + _Time.y*0.3f);
        o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
        o.color = (v.vertex.y-0.2f) / 8.0f;
        return o;
    }

    half4 frag (v2f i) : COLOR
    {
        return half4 (i.color, 1);
    }
    ENDCG

            }
        }
        Fallback "VertexLit"
}
