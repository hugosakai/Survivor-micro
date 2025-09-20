Shader "Custom/URP_SpriteHealthBar_NoDiscard"
{
    Properties
    {
        // IMPORTANTE: PerRendererData permite ao SpriteRenderer injetar a textura do sprite
        [PerRendererData] _BaseMap ("Sprite Texture", 2D) = "white" {}
        _BaseColor ("Tint", Color) = (1,1,1,1)
        _Fill ("Fill Amount", Range(0,1)) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "CanUseSpriteAtlas"="True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Name "Unlit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            // suporte a variantes comuns de sprite (atlas/compressões etc. — opcional, mas seguro)
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
                float4 color      : COLOR;     // cor do SpriteRenderer (inclui alpha)
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
                half4  color       : COLOR;
            };

            TEXTURE2D(_BaseMap); SAMPLER(sampler_BaseMap);
            float4 _BaseMap_ST;
            float4 _BaseColor;
            float  _Fill;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv          = TRANSFORM_TEX(v.uv, _BaseMap);
                o.color       = v.color;  // repassa a cor/alpha do sprite
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                half4 tex = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv);
                half4 col = tex * _BaseColor * i.color;

                // “corte” sem discard: zera pixels cuja uv.x > _Fill
                half m = step(i.uv.x, _Fill);
                col.rgb *= m;
                col.a   *= m;

                return col;
            }
            ENDHLSL
        }
    }
}
