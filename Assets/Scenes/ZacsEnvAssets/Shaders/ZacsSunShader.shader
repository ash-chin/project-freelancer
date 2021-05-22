Shader "Custom/ZacsSunShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _Emission("Emission", float) = 0
        _EmissionColor("Color", Color) = (0,0,0)
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

        
        fixed4 _EmissionColor;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {   
            float  lr = 250;
            float4 brighten = (0.8, 0.8, 0.8, 200);
            float4 lessRed = (0, 1, 1, 1);

            float3 localPos = IN.worldPos - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;

            fixed3 zero = (0, 0, 0);

            

            /*
            if (IN.uv_MainTex.x > 0.5)
                IN.uv_MainTex.x = 5*sin(IN.uv_MainTex.y*_Time.x);
            else
                IN.uv_MainTex.x = 5*(-sin(IN.uv_MainTex.y * _Time.x));
            */
            IN.worldPos.x += _Time.y;
            IN.uv_MainTex.x += _Time.x;

            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * (_Color * lessRed)*brighten;
            o.Albedo = c.rgb;

            
            o.Emission = c.rgb * tex2D(_MainTex, IN.uv_MainTex).a * _EmissionColor;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
