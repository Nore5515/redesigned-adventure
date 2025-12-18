Shader "LemonSpawn/LazyFog" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Scale ("Scale", Range(0,5)) = 1
        _Intensity ("Intensity", Range(0,1)) = 0.5
        _Alpha ("Alpha", Range(0,2.5)) = 0.75
        _AlphaSub ("AlphaSub", Range(0,1)) = 0.0
        _Pow ("Pow", Range(0,4)) = 1.0
    }
    SubShader {
        Tags {"Queue"="Transparent+101" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 400

        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            CBUFFER_START(UnityPerMaterial)
            float4 _Color;
            float4 _MainTex_ST;
            float _Scale;
            float _Intensity;
            float _Alpha;
            float _AlphaSub;
            float _Pow;
            CBUFFER_END
            
            struct Attributes {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
                float4 color : COLOR;
            };
            
            struct Varyings {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 positionWS : TEXCOORD1;
                float4 color : TEXCOORD2;
            };
            
            Varyings vert(Attributes input) {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                output.positionWS = TransformObjectToWorld(input.positionOS.xyz);
                output.color = input.color;
                return output;
            }
            
            float4 frag(Varyings input) : SV_Target {
                float3 worldSpacePosition = input.positionWS;
                float3 viewDirection = normalize(_WorldSpaceCameraPos - worldSpacePosition);
                
                float4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv * _Scale);
                float xx = c.r * _Intensity;
                xx = pow(xx, _Pow);
                c.a = c.r;
                c.rgb = float3(xx * _Color.r, xx * _Color.g, xx * _Color.b);
                c.a *= input.color.a - 2.5 * length(input.uv - float2(0.5, 0.5));
                c.a *= _Alpha;
                c.a -= _AlphaSub;
                return c;
            }
            ENDHLSL
        }
    }
}