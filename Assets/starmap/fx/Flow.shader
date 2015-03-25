Shader "Custom/Flow" {
    Properties {
        _FlowTex ("_FlowTex", 2D) = "white" {}
        _NoiseTex ("_NoiseTex", 2D) = "white" {}
        _NormTex0 ("_NormTex0", 2D) = "" {}
        _NormTex1 ("_NormTex1", 2D) = "" {}
    }
    SubShader {
        Pass {
            Tags { "RenderType"="Opaque" }
            Lighting Off Fog { Mode Off }
            LOD 200
           
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
 
                sampler2D _FlowTex, _NoiseTex, _NormTex0, _NormTex1;
                uniform float _FlowOffset0, _FlowOffset1, _HalfCycle;
               
                struct a2f {
                    float4 vertex : POSITION;
                    float4 texcoord : TEXCOORD0;
                };
 
                struct v2f {
                    float4 Pos : SV_POSITION;
                    float2 Uv : TEXCOORD0;
                };
 
                v2f vert (a2f v) {
                    v2f o;
                    o.Pos = mul(UNITY_MATRIX_MVP, v.vertex);
                    o.Uv = v.texcoord.xy;
                    return o;
                }
 
                half4 frag( v2f i ) : COLOR {
                    half2 flowTex = tex2D(_FlowTex, i.Uv).rg * 2.0f - 1.0f;
                    float cycleOffs = tex2D(_NoiseTex, i.Uv).r;
                   
                    //float phase0 = 0.5f + _FlowOffset0;
                    //float phase1 = 0.5f + _FlowOffset1;
                    float phase0 = cycleOffs * 0.5f + _FlowOffset0;
                    float phase1 = cycleOffs * 0.5f + _FlowOffset1;
                   
                    float3 norm0 = tex2D(_NormTex0, (i.Uv * 4) + flowTex * phase0);
                    float3 norm1 = tex2D(_NormTex1, (i.Uv * 2) + flowTex * phase1);
                   
                    float f = (abs(_HalfCycle - _FlowOffset0) / _HalfCycle);
                   
                    float3 normT = lerp(norm0, norm1, f);
                    //return float4(1.0f, 1.0f, 1.0f, 1.0f);
                    return float4(normT, 1.0f);
                }
            ENDCG
        }
    }
    FallBack Off
}