Shader "Custom/EnemyEyesShader" {
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
		//_BehindObjectColor ("BehindObjectColor", Color) = (0.66,0.66,0.66,1)
    }
    SubShader {
		/*Tags { "Queue"="Overlay+1" }
		Pass
		{
			//ZWrite Off
			//ZTest Greater
			//Lighting Off
			//Color [_BehindObjectColor]
		}
		*/
        Pass {
            Tags { "RenderType"="Opaque" }
       
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            sampler2D _MainTex;
            struct v2f {
                float4 pos : SV_POSITION;
                half2 uv : TEXCOORD0;
            };
 
            v2f vert(appdata_base v) {
                v2f o;
                v.vertex.x += sign(v.vertex.x) * sin(_Time.w)/200;
                v.vertex.y += sign(v.vertex.y) * cos(_Time.w)/200;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }
 
 
            half4 frag(v2f i) : COLOR {
                half4 c = tex2D(_MainTex, i.uv);
                return c;
            }
 
            ENDCG
        }
    }
    FallBack "Specular", 1
}