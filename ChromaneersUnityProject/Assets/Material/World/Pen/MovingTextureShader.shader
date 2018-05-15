Shader "Custom/MovingTextureShader" {
	Properties {
		_TintColor ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Transparency("Transparency", Range(0.0,0.5)) = 0.25
		_CutoutThresh("Cutout Threshhold", Range(0.0,1.0)) = 0.2
		_Distance("Distance", Float) = 1
		_Amplitude("Amplitude", Float) = 1
		_Speed("Speed", Float) = 1
		_Amount ("Amount", Range (0.0,1.0)) = 1
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_ScrollXSpeed ("X Scroll Speed", Range(0,10)) = 2
		_ScrollYSpeed ("Y Scroll Speed", Range(0,10)) = 2
	}
	SubShader {
		Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
		//CGPROGRAM
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
			struct Input{
				float2 uv_MainTex;
			};
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TintColor;
            float _Transparency;
            float _CutoutThresh;
            float _Distance;
            float _Amplitude;
            float _Speed;
            float _Amount;
			float _ScrollXSpeed;
			float _ScrollYSpeed;
            
            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
				fixed2 scrolledUV = i.uv;

				fixed xScrollValue = _ScrollXSpeed * _Time;
				fixed yScrollValue = _ScrollYSpeed * _Time;

				scrolledUV +=fixed2(xScrollValue,yScrollValue);

                fixed4 col = tex2D(_MainTex, scrolledUV) + _TintColor;
                col.a = _Transparency;
                clip(col - _CutoutThresh);
                return col;
            }
			ENDCG
        }/*
		Pass
		{
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			//#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			//#pragma target 3.0

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
			};

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf (Input IN, inout SurfaceOutputStandard o) {
				// Albedo comes from a texture tinted by color
				fixed2 scrolledUV = IN.uv_MainTex;

				fixed xScrollValue = _ScrollXSpeed * _Time;
				fixed yScrollValue = _ScrollYSpeed * _Time;

				scrolledUV += fixed2 (xScrollValue, yScrollValue);

				half4 c = tex2D(_MainTex, scrolledUV)*_TintColor;
				//o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				//o.Metallic = _Metallic;
				//o.Smoothness = _Glossiness;
				//o.Alpha = c.a;
			}
			ENDCG
		}*/
	}
	FallBack "Diffuse"
}
