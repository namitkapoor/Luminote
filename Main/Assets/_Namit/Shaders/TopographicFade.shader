Shader "Custom/DistanceBasedColorWithTopography"
{
    Properties
    {
        _LineColor("Line Color", Color) = (0,0,0,1)
        _BaseColorNear("Base Color Near", Color) = (1,1,1,1)
        _BaseColorFar("Base Color Far", Color) = (0.5,0.5,0.5,1)
        _LineSpacing("Line Spacing", float) = 1
        _LineWidth("Line Width", float) = 0.1
        _FadeStart("Fade Start Distance", float) = 1
        _FadeEnd("Fade End Distance", float) = 10
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
                float4 screenPos : SV_POSITION;
                float camDistance : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.screenPos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                // Compute the camera distance in the vertex shader for better performance
                o.camDistance = length(_WorldSpaceCameraPos - o.worldPos);
                return o;
            }

            float4 _LineColor;
            float4 _BaseColorNear;
            float4 _BaseColorFar;
            float _LineSpacing;
            float _LineWidth;
            float _FadeStart;
            float _FadeEnd;

            fixed4 frag(v2f i) : SV_Target
            {
                // Calculate the fade based on camera distance
                float fade = saturate((i.camDistance - _FadeStart) / (_FadeEnd - _FadeStart));
                float4 baseColor = lerp(_BaseColorNear, _BaseColorFar, fade);

                // Calculate the topographic lines
                float dp = dot(i.worldPos, float3(0,1,0)); // dot product with world up
                float band = fmod(abs(dp), _LineSpacing);
                float lineStrength = smoothstep(_LineWidth, _LineWidth * 0.5, band);

                // Overlay the topographic lines over the base color
                return lerp(baseColor, _LineColor, lineStrength);
            }
            ENDCG
        }
    }
}
