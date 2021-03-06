Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "pink" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _Saturation("Saturation", Range(0,4)) = 0.0
        _Multiplier("Multiplier", Range(0,100)) = 1

    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;
            half _Glossiness;
            half _Metallic, _Saturation, _Multiplier;
            fixed4 _Color;  //vector -> rgba

            struct Input
            {
                float2 uv_MainTex;
            };

            // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
            // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
            // #pragma instancing_options assumeuniformscaling
            UNITY_INSTANCING_BUFFER_START(Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END(Props)

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                float2 uv = IN.uv_MainTex;


                //uv.y += sin(uv.y * _Multiplier +_Time.y)*.2;
                //uv.x += sin(uv.y * _Multiplier + _Time.y)*.1;
                // Albedo comes from a texture tinted by color
                fixed4 c = tex2D(_MainTex, uv) * _Color;

                float saturation = sin(uv.y * 7);
                o.Albedo = lerp(float4(0, -sin(c.g*c.b * _Time.y), sin(c.b*c.g*_Time.y), 0)*.5 ,c, sin(_Time.y*.5)*20);
                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}

//c.g + c.b + c.r + _Time.y * 3)+.2