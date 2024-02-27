Shader "Custom/CameraSpaceNormals"
{
    Properties
    {
        _Multiply("Multiply", float) = 1
        _Add("Add", float) = 1
        _ColorA("Color A ", color) = (1,1,1,1)
        _ColorB("Color B", color) = (1,1,1,1)
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Opacity("Opacity", Range(0, 1)) = 1.0 // New opacity property
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
                    float3 normal : TEXCOORD0;
                    float4 pos : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.normal = mul((float3x3)UNITY_MATRIX_MV, v.normal);
                    return o;
                }

                sampler2D _MainTex;
                float _Multiply;
                float _Add;
                float4 _ColorA;
                float4 _ColorB;
                float _Opacity; // Declare opacity variable

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed3 normalColor = i.normal * 0.5 + 0.5;
                    float edge = normalColor.z * _Multiply + _Add;
                    float4 outColor = lerp(_ColorA, _ColorB, saturate(edge));
                    outColor.a *= _Opacity; // Apply opacity to the alpha channel
                    return outColor;
                }
                ENDCG
            }
        }
}
