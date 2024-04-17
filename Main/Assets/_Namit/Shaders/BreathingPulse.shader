Shader "Custom/BreathingPulseShaderWithNormals"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _EdgeColor("Edge Color", Color) = (1,0,0,1)
        _EdgeWidth("Edge Width", Float) = 0.01
        _PulseSpeed("Pulse Speed", Float) = 1.0
        _LightDirection("Light Direction", Vector) = (-1,-1,-1,0)
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                float4 _EdgeColor;
                float _EdgeWidth;
                float _PulseSpeed;
                float3 _LightDirection;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.normal = UnityObjectToWorldNormal(v.normal);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Normalize the light direction
                    float3 lightDir = normalize(_LightDirection);

                    // Calculate the dot product
                    float dotNL = max(0, dot(i.normal, lightDir));
                    float lighting = smoothstep(0.0, 1.0, dotNL);

                    float2 uv = i.uv;

                    // Apply pulse effect to base texture color
                    float pulse = sin(_Time.y * _PulseSpeed) * 0.5 + 0.5;
                    fixed4 texColor = tex2D(_MainTex, uv) * pulse * lighting; // Multiply by lighting effect

                    // No edge detection in this version, but could be added similarly
                    fixed4 color = texColor;

                    // Optionally mix with edge color based on an edge detection logic
                    // fixed4 color = lerp(texColor, _EdgeColor * lighting, edge);

                    return color;
                }
                ENDCG
            }
        }
}
