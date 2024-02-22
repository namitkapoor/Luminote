Shader "Custom/CameraSpaceNormals"
{
    Properties
    {
        _Multiply("Multiply", float ) = 1
        _Add("Add", float) = 1
        _ColorA("Color A " , color) = (1,1,1,1)
        _ColorB("Color B", color) = (1,1,1,1)
        _MainTex("Base (RGB)", 2D) = "white" {}
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
                // Transform the normal from object space to camera space
                o.normal = mul((float3x3)UNITY_MATRIX_MV, v.normal);
                return o;
            }

            sampler2D _MainTex;
            float _Multiply;
            float _Add;
            float4 _ColorA;
            float4 _ColorB;

            fixed4 frag(v2f i) : SV_Target
            {
                // Use the camera space normal in your calculations
                // This example simply visualizes the normal as color
                fixed3 normalColor = i.normal * 0.5 + 0.5; // Normalize to 0-1
                float edge = normalColor.z*_Multiply+_Add;
                float4 outColor = lerp(_ColorA, _ColorB, saturate(edge));
                return outColor;    
                //return fixed4(normalColor, 1.0);
            }
            ENDCG
        }
    }
}
