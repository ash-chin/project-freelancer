Shader "Unlit/ArthShield"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color (RGBA)",Color) = (1,1,1,1)
        _OtherCol("Color (RGBA", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        //Cull front
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert alpha
            #pragma fragment frag alpha
                 

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _OtherCol;

            v2f vert (appdata v)
            {
                v2f o;
                
                if (floor(v.vertex.x) == floor(sqrt(-pow((v.vertex.y), 2) + 1)))
                   v.uv.x += _Time.x;
                
                else if (floor(v.uv.x) == floor(sqrt(-pow((v.uv.y), 2) + 1)))
                   v.uv.x += _Time.x;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                // sample the texture
                //i.uv.x += sin(i.uv.y * _Time.y);
                i.uv.y += _Time.x;
              

                fixed4 col = tex2D(_MainTex, i.uv);
                
                
                col.x *= _OtherCol.x;
                col.y *= _OtherCol.y;
                col.z *= _OtherCol.z;
                
                col.w = sin(_OtherCol.w * _Time.y*2);

                

                
                
                if (!all(col))
                    col = _Color;

                return col;
            }
            ENDCG
        }
    }
}
