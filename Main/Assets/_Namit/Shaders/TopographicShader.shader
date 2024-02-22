Shader "Custom/TopographicShaderBasedOnCameraNormals"
{
    Properties
    {
        _Multiply("Multiply", float) = 1
        _Add("Add", float) = 0
        _LineColor("Line Color", Color) = (0,0,0,1)
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _LineSpacing("Line Spacing", float) = 1
        _LineWidth("Line Width", float) = 0.1
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
                // Transform the normal and position from object space to world space
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

            fixed4 frag(v2f i) : SV_Target
            {
                // Calculate the dot product between the world position and the camera's up vector
                float3 camUp = float3(0, 1, 0); // Assuming Y is up in world space
                float dp = dot(i.worldPos, camUp);

                // Normalize the value and scale by the line spacing
                float lineValue = fmod(abs(dp) * _Multiply + _Add, _LineSpacing);
                lineValue = saturate(1 - lineValue / _LineWidth);

                // Lerp between the base color and line color based on the line value
                float4 color = lerp(_BaseColor, _LineColor, lineValue);

                // Use the camera space normal in your calculations
                float3 normalDirection = normalize(i.worldNormal);
                float edge = (normalDirection.z - 100)* _Multiply + _Add;

                // Blend the base color and line color based on the edge value
                color = lerp(color, _LineColor, saturate(edge));

                return color;
            }
            ENDCG
        }
    }
}
