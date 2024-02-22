Shader "Custom/BlendedPulseShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BlendTex("Blend Texture", 2D) = "white" {}
        _Blend("Blend Factor", Range(0,1)) = 0.5
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows

            sampler2D _MainTex;
            sampler2D _BlendTex;
            float _Blend;

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                half4 c = tex2D(_MainTex, IN.uv_MainTex);
                half4 blendColor = tex2D(_BlendTex, IN.uv_MainTex);
                c = lerp(c, blendColor, _Blend);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
