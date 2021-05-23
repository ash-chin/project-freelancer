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
            #pragma fragment frag //alpha
                 

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
                
                //v.uv.x += _Time.y;
                //v.uv.x += _Time.y/2;
                v.vertex.y += sin(v.vertex.y + _Time.y)*.05;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                // sample the texture
                i.uv.x += _Time.x;
                
                i.uv.y += sin(i.uv.y*_Time.y);
                fixed4 col = tex2D(_MainTex, i.uv);
                
                
                
                col *= _OtherCol;
                
                //col.w *= _Color.w;
                
                if (!all(col))
                    col = _Color;

                return col;
            }
            ENDCG
        }
    }
}
