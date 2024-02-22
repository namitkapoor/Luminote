Shader "Custom/FrostedGlass"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BlurSize("Blur Size", Float) = 1.0
    }
        SubShader
        {
            // Transparent - render after opaque objects
            Tags { "Queue" = "Transparent" }

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float _BlurSize;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float2 uv = i.uv;
                    fixed4 col = fixed4(0,0,0,0);

                    // Sample the texture at multiple points
                    for (int x = -2; x <= 2; x++)
                    {
                        for (int y = -2; y <= 2; y++)
                        {
                            float2 offset = float2(x, y) * _BlurSize * _ScreenParams.zw;
                            col += tex2D(_MainTex, uv + offset);
                        }
                    }

                    // Average the color
                    col /= 25.0;

                    return col;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}
