Shader "Custom/TopographicShaderWithAnimatedCurvedLines"
{
    Properties
    {
        _Multiply("Multiply", float) = 1
        _Add("Add", float) = 0
        _LineColor("Line Color", Color) = (0,0,0,1)
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _LineSpacing("Line Spacing", float) = 1
        _LineWidth("Line Width", float) = 0.1
        _OcclusionStrength("Occlusion Strength", float) = 1
        _OcclusionScale("Occlusion Scale", float) = 1
        _NoiseScale("Noise Scale", float) = 1
        _NoiseIntensity("Noise Intensity", float) = 1
        _AnimationSpeed("Animation Speed", float) = 1
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
            };

            struct v2f
            {
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float _Multiply;
            float _Add;
            float4 _LineColor;
            float4 _BaseColor;
            float _LineSpacing;
            float _LineWidth;
            float _OcclusionStrength;
            float _OcclusionScale;
            float _NoiseScale;
            float _NoiseIntensity;
            float _AnimationSpeed;

            fixed4 frag(v2f i) : SV_Target
            {
                // Animated topographic line calculations with noise for curvature
                float3 camUp = float3(0, 1, 0);
                float animatedAdd = _Add + _Time.y * _AnimationSpeed;
                float dp = dot(i.worldPos, camUp);
                dp += sin(i.worldPos.x * _NoiseScale + _Time.y * _AnimationSpeed) * _NoiseIntensity; // Add animated noise to the line value
                float lineValue = fmod(abs(dp) * _Multiply + animatedAdd, _LineSpacing);
                lineValue = saturate(1 - lineValue / _LineWidth);
                float4 color = lerp(_BaseColor, _LineColor, lineValue);

                // Ambient Occlusion-like effect
                float3 ambientOcclusion = saturate(dot(normalize(i.worldNormal), camUp) * _OcclusionScale + _OcclusionStrength);
                color.rgb *= ambientOcclusion;

                return color;
            }
            ENDCG
        }
    }
}
