Shader "Custom/URP2D_SpriteHealthBar_Compat"
{
    Properties
    {
        [PerRendererData] _MainTex   ("Sprite Texture", 2D) = "white" {}
                          _BaseMap   ("Base Map (alt)", 2D) = "white" {}
                          _Color     ("Tint", Color) = (1,1,1,1)
                          _BaseColor ("Tint (alt)", Color) = (1,1,1,1)
                          _Fill      ("Fill Amount", Range(0,1)) = 1
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
        ZTest Always

        // Pass para o 2D Renderer da URP
        Pass
        {
            Name "SpriteUnlit2D"
            Tags { "LightMode"="Universal2D" }

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
                float4 color      : COLOR;
            };
            struct Varyings {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
                half4  color       : COLOR;
            };

            // texturas (suportar ambos nomes)
            TEXTURE2D(_MainTex);  SAMPLER(sampler_MainTex);
            TEXTURE2D(_BaseMap);  SAMPLER(sampler_BaseMap);
            float4 _MainTex_ST, _BaseMap_ST;

            // cores (suportar ambos nomes)
            float4 _Color, _BaseColor;
            float  _Fill;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv          = v.uv; // sprites já vêm em UV 0..1
                o.color       = v.color;
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                // amostra de ambas (se _BaseMap não estiver ligado, será branco)
                half4 texA = SAMPLE_TEXTURE2D(_MainTex,  sampler_MainTex,  i.uv);
                half4 texB = SAMPLE_TEXTURE2D(_BaseMap,  sampler_BaseMap,  i.uv);
                half4 tex  = texA * texB;

                // escolhe cor (qualquer uma que vier != branca)
                half4 tint = _Color * _BaseColor;

                half4 col = tex * tint * i.color;

                // “corte” pelo fill (sem discard)
                half m = step(i.uv.x, _Fill);
                col.rgb *= m;
                col.a   *= m;

                return col;
            }
            ENDHLSL
        }
    }
}
