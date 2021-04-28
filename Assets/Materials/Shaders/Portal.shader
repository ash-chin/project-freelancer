Shader "Unlit/Portal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OscillationSpeed("Oscillation speed", Range(1,5)) = 1
        _OscillationDistance("Oscillation distance", Range(1,50)) = 1
        _Color("Color (RGBA)",Color) = (1,1,1,1) //add color property
        _XTexSpeed("X texture speed",Range(1,50)) = 1
        _YTexSpeed("Y texture speed",Range(1,50)) = 1

    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull front
        LOD 100

        Pass
        {
            CGPROGRAM
            // make fog work
            #pragma multi_compile_fog
            #pragma vertex vert alpha
            #pragma fragment frag alpha
            

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
            float4 _Color;
            half _XTexSpeed;
            half _YTexSpeed;

            //vertex shaderd
            v2f vert (appdata v)
            {
                
                v2f o;
                v.vertex.y = sin((v.vertex.y+_Time.y)*_OscillationSpeed)*_OscillationDistance;
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
                i.uv.x += _Time.y * _XTexSpeed; //sin(i.uv.y  + _Time.y)*_XTexSpeed;
                i.uv.y += sin(i.uv.y + _Time.y*.5) * _YTexSpeed;
        
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                
                return float4(0,0, .2+sin(_Time.y*.5)*.2, 3+2*sin(_Time.y*2)) + col;
            }
            ENDCG
        }
    }
}
//return float4(sin(_Time.y * 200), .6, sin(_Time.y * 200) * 500, -sin(_Time.y * 200) * 200) - .7 + col;
//
// 
// pink fade in occasionally after normal uv mapping:
// return float4(sin(_Time.y), .6, sin(_Time.y), -sin(_Time.y) )-.7 + col;
//

//float4(-0.1, -.2, .1 + cos(_Time.y * .5), 0) + col

/*original uv code:
* i.uv.x += sin(i.uv.y  + _Time.y)*5;
 * i.uv.y += sin(i.uv.y + _Time.y*.5) * 5;
 */