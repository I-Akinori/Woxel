Shader "Custom/Woxel"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        [Toggle] _Top ("Top", Float) = 0
        [Toggle] _Bottom ("Bottom", Float) = 0
        _Snow ("Snow", Range(0, 1)) = 1
        _Light ("Light", Range(0, 1)) = 1
        _Rain ("Rain", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half _Top;
        half _Bottom;
        half _Snow;
        half _Rain;
        half _Light;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        float3 HueToRGB(float h)
        {
            float3 rgb = saturate(abs(frac(h + float3(0, 2.0/3.0, 1.0/3.0)) * 6.0 - 3.0) - 1.0);
            return rgb;
        }
        float rand(float2 co) //引数はシード値と呼ばれる　同じ値を渡せば同じものを返す
        {
            return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
        }
        float4 Light (float2 uv, float3 wp)
        {
            float4 lit = float4(0, 0, 0, 1);
            float v = ((wp.x + wp.y * 0.5) * 0.2 - _Time.y * 0.1) % 1;
            lit.xyz += HueToRGB(v) * 0.05;
            for (uint i = 0; i < 10; i++)
            {
                float2 pos = 0.05 + 0.9 * float2(rand(float2(123 - i, 456 + i)), rand(float2(987 + i, i)));
                float dist = length(uv - pos);
                if (dist < 0.2)
                {
                    float pi = 3.141592;
                    float bright = exp(-dist * 50) * (sin(2 * _Time.y + (i % 2 == 0 ? 0 : pi)) * 0.5 + 0.5);
                    lit.xyz += HueToRGB(rand(pos)) * bright;
                }
            }
            
            return lit * _Light;
        }
        float4 Snow (float2 uv, float4 c)
        {
            if (_Top > 0.5f)
            {
                float sub = uv.y;
                sub -= 1 - cos(uv.x * 3.141592 * 4) * 0.1
                       - sin(uv.x * 3.141592 * 2) * 0.05
                       - 0.3 * _Snow;
                if (sub > 0)
                {
                    c.rgb = lerp(c.rgb, float3(1, 1, 1), _Snow * saturate(sub * 8));
                }
            }
            if (_Bottom > 0.5f)
            {
                float sub = - uv.y;
                sub += cos(uv.x * 3.141592 * 4) * 0.02
                    + sin(uv.x * 3.141592 * 2) * 0.01
                       + 0.2 * _Snow;
                if (sub > 0)
                {
                    c.rgb = lerp(c.rgb, float3(1, 1, 1), _Snow * saturate(sub * 8));
                }
            }
            return c;
        }
        float4 Rain (float2 uv, float4 c)
        {
            float sub = uv.y + _Time.y;
            float wave = 0;
            wave += cos(uv.x * 3.141592 * 4) * 0.05;
            wave -= sin(uv.x * 3.141592 * 2 + _Time.y) * 0.06;
            if (_Top > 0.5f)
            {
                wave *= saturate((1 - uv.y) * 2);
            }
            if (_Bottom > 0.5f)
            {
                wave *= saturate(uv.y * 2);
            }
            sub -= wave;
            
            sub = (sub + 1) % 0.5;
            if (sub < 0.2)
            {
                sub = 1 - sub * 5;
                c.rgb = lerp(c.rgb, float3(1, 1, 1), _Rain * sub);
            }
            return c;
        }
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            float2 uv = IN.uv_MainTex;
            c = Snow(uv, c);
            c = Rain(uv, c);
            float4 light = Light(uv, IN.worldPos);
            c += light;
            
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Emission = light;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
