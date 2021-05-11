Shader "Custom/StandardPearl"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _ShinyExp("Shininess Exponent", Range(0.0,6.0)) = 1.0
        _ShinyCoeff("Shininess Coefficient", Range(0.0,100.0)) = 1.0
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
            float3 viewDir;
            float3 worldRefl;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _ShinyExp;
        float _ShinyCoeff;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float3 Norm(float3 v)
        {
            float sqrRoot = sqrt(dot(v, v));
            if(sqrRoot>0)
                return v/sqrt(dot(v, v));

            return v;

        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            
            float lighting = 0;

            //diffuse lighting based on viewDir
            float diffAmt = 50.0f * dot(Norm(o.Normal) ,Norm(IN.viewDir));
            lighting += diffAmt;
            //specular lighting based on viewDir and lightDir and light reflection

            
            float specAmt = _ShinyCoeff * pow(max(0.0, dot(IN.worldRefl, IN.viewDir)), _ShinyExp);
            lighting += specAmt;

            lighting += diffAmt;

            //use lighting calculation to move uv map to get some interesting effects
            float uvMove = (lighting / 400) + 0.4*_Time.x;
            IN.uv_MainTex.x +=  uvMove;
            IN.uv_MainTex.y += -uvMove;
            
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color * lighting;
            o.Albedo = c.rgb;
            
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
