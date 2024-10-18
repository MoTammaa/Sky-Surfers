Shader "Custom/DoubleSidedShader" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _BumpMap ("Normal Map", 2D) = "bump" {}
    }
    SubShader {
        Tags {"RenderType"="Opaque"}
        LOD 200
        Cull Off
        
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows addshadow
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        struct Input {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            if (dot(o.Normal, IN.viewDir) < 0.0)
                o.Normal = -o.Normal;
            o.Alpha = c.a;
        }
        ENDCG
    }
    Fallback "Diffuse"
}
