Shader "Custom/PulseShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _PulseStrength("Pulse Strength", Float) = 1.0
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
                float3 normal : NORMAL;
                float4 vertex : SV_POSITION;
            };

            uniform float4 _Color;
            uniform float _PulseStrength;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float intensity = abs(sin(_Time.y * _PulseStrength));
                return _Color * intensity;
            }
            ENDCG
        }
    }
}
