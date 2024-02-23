Shader "Custom/DistanceBasedColor"
{
    Properties
    {
        _HitColor("Hit Color", Color) = (0,1,0,1)
        _FarColor("Far Color", Color) = (1,0,0,1)
        _HitPosition("Hit Position", Vector) = (0,0,0)
        _DistanceThreshold("Distance Threshold", Float) = 5.0
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #include "UnityCG.cginc"

        struct Input
        {
            float3 worldPos;
        };

        float3 _HitPosition;
        float _DistanceThreshold;
        fixed4 _HitColor;
        fixed4 _FarColor;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Calculate the distance from the hit position
            float dist = distance(IN.worldPos, _HitPosition); // Use 'dist' to avoid name clash

            // Normalize the distance based on the threshold
            float normalizedDistance = saturate(1.0 - dist / _DistanceThreshold);

            // Lerp between the far color and the hit color based on the normalized distance
            fixed4 color = lerp(_FarColor, _HitColor, normalizedDistance);

            // Output the color
            o.Albedo = color.rgb;
            o.Alpha = 1.0;
        }

        ENDCG
    }
        FallBack "Diffuse"
}
