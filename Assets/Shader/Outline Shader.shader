Shader "Custom/Outline Shader"
{
    Properties
    {
        _OutlineColor("Outline Color", Color) = (1,1,0,1)
        _OutlineThickness("Outline Thickness", Range(0.0, 0.1)) = 0.02
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }

        Pass
        {
            Name "Outline"
            Tags { "LightMode"="UniversalForward" }

            Cull Front   // 앞면을 버리고 뒷면만 그림 → 외곽선처럼 보임

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
            };

            float _OutlineThickness;
            float4 _OutlineColor;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                float3 normalWS = normalize(TransformObjectToWorldNormal(IN.normalOS));
                float3 positionWS = TransformObjectToWorld(IN.positionOS.xyz);

                // 노멀 방향으로 밀어서 외곽선 효과
                positionWS += normalWS * _OutlineThickness;

                OUT.positionHCS = TransformWorldToHClip(positionWS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                return _OutlineColor;
            }
            ENDHLSL
        }
    }
}
