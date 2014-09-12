Shader "Custom/Buttons"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            // Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members screenPos)
            #pragma exclude_renderers d3d11 xbox360
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON

            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                half2 texcoord  : TEXCOORD0;
                float4 screenPos;
            };
            
            fixed4 _Color;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                OUT.screenPos = ComputeScreenPos(OUT.vertex);

                return OUT;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f IN) : SV_Target
            {
                float y = IN.screenPos.y * _ScreenParams.y;
                float x = IN.screenPos.x * _ScreenParams.x;
                fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
                c.rgb *= 0.9 + 0.1 * fmod(y, 2); // * fmod(x, 2);
                c.rgb *= 0.9 + (1-clamp(fmod(IN.screenPos.y*5 + _Time.y, 10)/10, 0.9, 1)) * fmod(y, 2); //* fmod(x, 2);
                c.rgb *= c.a;
                return c;
            }
        ENDCG
        }
    }
}