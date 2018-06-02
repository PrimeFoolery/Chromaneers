Shader "Custom/OutlineGlowAndDissolveShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline Width", Range (0.0, 3.0)) = 0.005

		  _EmissionMap ("Emission Map", 2D) =  "black"{}
  [HDR] _EmissionColor ("Emission Color", Color) = (0,0,0)

  
        _SliceGuide("Slice Guide (RGB)", 2D) = "white" {}
        _SliceAmount("Slice Amount", Range(0.0, 1.0)) = 0
 
        _BurnSize("Burn Size", Range(0.0, 1.0)) = 0.15
        _BurnRamp("Burn Ramp (RGB)", 2D) = "white" {}
        _BurnColor("Burn Color", Color) = (1,1,1,1)
		_EmissionAmount("Emission amount", float) = 2.0
	}
	    CGINCLUDE
        #define UNITY_SETUP_BRDF_INPUT MetallicSetup
		#include "UnityCG.cginc"
		struct appdata {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};
 
struct v2f {
	float4 pos : POSITION;
	float4 color : COLOR;
};
 
uniform float _Outline;
uniform float4 _OutlineColor;
float _EmissionAmount;
 
v2f vert(appdata v) {
	// just make a copy of incoming vertex data but scaled according to normal direction
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);
 
	float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
	float2 offset = TransformViewToProjection(norm.xy);
 
	o.pos.xy += offset * o.pos.z * _Outline;
	o.color = _OutlineColor;
	return o;
}
    ENDCG
		
	SubShader {
	
	
	Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert addshadow

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		        sampler2D _SliceGuide;
        sampler2D _BumpMap;
        sampler2D _BurnRamp;
        fixed4 _BurnColor;
        float _BurnSize;
        float _SliceAmount;

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};


		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float4 _EmissionColor;
		sampler2D _EmissionMap;

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			half test = tex2D(_SliceGuide, IN.uv_MainTex).rgb - _SliceAmount;
            clip(test);
             
            if (test < _BurnSize && _SliceAmount > 0) {
                o.Emission = tex2D(_BurnRamp, float2(test * (1 / _BurnSize), 0)) * _BurnColor * _EmissionAmount;
            }
			o.Alpha = c.a;
            o.Albedo = c.rgb;
			o.Albedo = c.rgb;
			
			half4 emission = tex2D(_EmissionMap, IN.uv_MainTex)*_EmissionColor;
			o.Emission = o.Emission + emission.rgb;
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			
            
		}


		ENDCG
	
		Pass {
		Tags { "Queue" = "Transparent" "RenderType" =  "Transparent"}
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite Off
			ZTest LEqual
			ColorMask RGB // alpha not used
 
			// you can choose what kind of blending mode you want for the outline
			Blend SrcAlpha OneMinusSrcAlpha // Normal
			//Blend One One // Additive
			//Blend One OneMinusDstColor // Soft Additive
			//Blend DstColor Zero // Multiplicative
			//Blend DstColor SrcColor // 2x Multiplicative
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
 
			 fixed4 _Color;
			    sampler2D _MainTex2;
			   sampler2D _SliceGuide2;
			   sampler2D _BumpMap2;
			    sampler2D _BurnRamp2;
			    fixed4 _BurnColor2;
			   float _BurnSize2;
			   float _SliceAmount2;
			   float _EmissionAmount2;
 
			struct Input {
				float2 uv_MainTex;
			};
		
			half4 frag(v2f i) :COLOR {
				return i.color;
			}
			ENDCG
		}
	}
	/*SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off
        CGPROGRAM
        #pragma surface surf Lambert addshadow
        #pragma target 3.0
 
        fixed4 _Color;
        sampler2D _MainTex;
        sampler2D _SliceGuide;
        sampler2D _BumpMap;
        sampler2D _BurnRamp;
        fixed4 _BurnColor;
        float _BurnSize;
        float _SliceAmount;
        float _EmissionAmount;
 
		struct appdata {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};
 
struct v2f {
	float4 pos : POSITION;
	float4 color : COLOR;
};
 
uniform float _Outline;
uniform float4 _OutlineColor;

        struct Input {
            float2 uv_MainTex;
        };
 
 
		v2f vert(appdata v) {
	// just make a copy of incoming vertex data but scaled according to normal direction
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);
 
	float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
	float2 offset = TransformViewToProjection(norm.xy);
 
	o.pos.xy += offset * o.pos.z * _Outline;
	o.color = _OutlineColor;
	return o;
}
        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            half test = tex2D(_SliceGuide, IN.uv_MainTex).rgb - _SliceAmount;
            clip(test);
             
            if (test < _BurnSize && _SliceAmount > 0) {
                o.Emission = tex2D(_BurnRamp, float2(test * (1 / _BurnSize), 0)) * _BurnColor * _EmissionAmount;
            }
 
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }*/
		
 
		// note that a vertex shader is specified here but its using the one above
		
	FallBack "Diffuse"
}
