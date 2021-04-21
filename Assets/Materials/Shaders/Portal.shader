Shader "Unlit/Portal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OscillationSpeed("Oscillation speed", Range(1,5)) = 1
        _OscillationDistance("Oscillation distance", Range(1,50)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half _OscillationSpeed, _OscillationDistance;

            //vertex shader
            v2f vert (appdata v)
            {
                
                v2f o;

                v.vertex.y += sin((v.vertex.y+_Time.y)*_OscillationSpeed)*_OscillationDistance;
                //v.vertex.x += sin((v.vertex.y + _Time.y) * _OscillationSpeed) * _OscillationDistance;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            //pixel shader /fragment shader
            fixed4 frag(v2f i) : SV_Target
            {

                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                /*
                float2 uv = i.uv - .5;
                float a = _Time.y;
                float2 p = float2(sin(a),cos(a))*.4;
                float distort = uv - p;
                float d = length(distort);
                float m = smoothstep(.02, 0, d);
                distort = distort * 20 * m;
                */
                float4 pink = float4(1, 0.75, 0.8, 1);
                i.uv.x += sin(i.uv.y  + _Time.y)*5;
                i.uv.y += sin(i.uv.y + _Time.y*.5) * 5;
        
                fixed4 col = tex2D(_MainTex, i.uv);
                
                return col;
            }
            ENDCG
        }
    }
}
