Shader "Custom/URP2D_SpriteHealthBar_NoDiscard"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color   ("Tint", Color) = (1,1,1,1)
        _Fill    ("Fill Amount", Range(0,1)) = 1
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
            Name "SpriteUnlit"
            Tags { "LightMode"="Universal2D" }   // <- ESSENCIAL para 2D Renderer

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

            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;
            float4 _Color;
            float  _Fill;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv          = TRANSFORM_TEX(v.uv, _MainTex);
                o.color       = v.color;
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                half4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                half4 col = tex * _Color * i.color;

                // corte por alpha, sem discard
                half m = step(i.uv.x, _Fill);
                col.rgb *= m;
                col.a   *= m;

                return col;
            }
            ENDHLSL
        }

        // (Opcional) Passo extra para cenas não-2D:
        Pass
        {
            Name "DefaultUnlit"
            Tags { "LightMode"="SRPDefaultUnlit" }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off ZWrite Off

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            struct Attributes { float4 positionOS:POSITION; float2 uv:TEXCOORD0; float4 color:COLOR; };
            struct Varyings  { float4 positionHCS:SV_POSITION; float2 uv:TEXCOORD0; half4 color:COLOR; };
            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            float4 _MainTex_ST; float4 _Color; float _Fill;
            Varyings vert(Attributes v){ Varyings o; o.positionHCS=TransformObjectToHClip(v.positionOS.xyz); o.uv=TRANSFORM_TEX(v.uv,_MainTex); o.color=v.color; return o; }
            half4 frag(Varyings i):SV_Target{ half4 tex=SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.uv); half4 col=tex*_Color*i.color; half m=step(i.uv.x,_Fill); col.rgb*=m; col.a*=m; return col; }
            ENDHLSL
        }
    }
}
