Shader "NotSoGreeeen/Quantization (16)"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        
        _Color1 ("Color", Color) = (0, 0, 0, 0)
        _Color2 ("Color", Color) = (0, 0, 0, 0)
        _Color3 ("Color", Color) = (0, 0, 0, 0)
        _Color4 ("Color", Color) = (0, 0, 0, 0)
        _Color5 ("Color", Color) = (0, 0, 0, 0)
        _Color6 ("Color", Color) = (0, 0, 0, 0)
        _Color7 ("Color", Color) = (0, 0, 0, 0)
        _Color8 ("Color", Color) = (0, 0, 0, 0)
        _Color9 ("Color", Color) = (0, 0, 0, 0)
        _Color10 ("Color", Color) = (0, 0, 0, 0)
        _Color11 ("Color", Color) = (0, 0, 0, 0)
        _Color12 ("Color", Color) = (0, 0, 0, 0)
        _Color13 ("Color", Color) = (0, 0, 0, 0)
        _Color14 ("Color", Color) = (0, 0, 0, 0)
        _Color15 ("Color", Color) = (0, 0, 0, 0)
        _Color16 ("Color", Color) = (0, 0, 0, 0)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float4 _Color4;
            float4 _Color5;
            float4 _Color6;
            float4 _Color7;
            float4 _Color8;
            float4 _Color9;
            float4 _Color10;
            float4 _Color11;
            float4 _Color12;
            float4 _Color13;
            float4 _Color14;
            float4 _Color15;
            float4 _Color16;

            float4 _Colors[16] =
            {
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1),
                float4(1, 1, 1, 1)
            };

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 Quantize(float4 col)
            {
                float dist = distance(_Colors[0], col);
                float4 tempColor = _Colors[0];

                for (int i = 1; i < 16; i++)
                {
                    if (distance(_Colors[i], col) < dist)
                    {
                        dist = distance(_Colors[i], col);
                        tempColor = _Colors[i];
                    }
                }

                return tempColor;
            }

            void changeArray()
            {
                _Colors[0] = _Color1;
                _Colors[1] = _Color2;
                _Colors[2] = _Color3;
                _Colors[3] = _Color4;
                _Colors[4] = _Color5;
                _Colors[5] = _Color6;
                _Colors[6] = _Color7;
                _Colors[7] = _Color8;
                _Colors[8] = _Color9;
                _Colors[9] = _Color10;
                _Colors[10] = _Color11;
                _Colors[11] = _Color12;
                _Colors[12] = _Color13;
                _Colors[13] = _Color14;
                _Colors[14] = _Color15;
                _Colors[15] = _Color16;
            }

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                // just invert the colors NO I NO LISTEN TO YOU!! >:(
                changeArray();
                if (col.a != 0)
                {
                    col = Quantize(col);
                } else
                {
                    clip(-1);
                }
                
                return col;
            }
            ENDCG
        }
    }
}
