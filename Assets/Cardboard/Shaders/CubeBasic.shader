	Shader "MonsterCave/Basic Cube" {
	Properties {
		_Color ("Main Color", Color) = (.1,.1,.1,1)
		//_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		//_Outline ("Outline width", Range (.002, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" { }
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos : SV_POSITION;
		//UNITY_FOG_COORDS(0)
		fixed4 color : COLOR;
	};
	
	//uniform float _Outline;
	//uniform float4 _OutlineColor;
	
	
	
	v2f vert(appdata v) {
		v2f o;
		//v.vertex.xyz *= (1 + _Outline);//scale the vertices so the cube will be bigger
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);


		//float3 norm   = normalize(mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal));
		//float2 offset = TransformViewToProjection(norm.xy);

		//o.pos.xy += offset * o.pos.z * _Outline;
		//o.color = _OutlineColor;
		//UNITY_TRANSFER_FOG(o,o.pos);
		return o;
	}
	ENDCG
	
	
 
	SubShader {
		//Tags {"Queue" = "Geometry+100" }
	CGPROGRAM
	#pragma surface surf Lambert
	 
	sampler2D _MainTex;
	fixed4 _Color;
	 
	struct Input {
		float2 uv_MainTex;
	};
	 
	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	 
			// note that a vertex shader is specified here but its using the one above
			Pass {
				Name "OUTLINE"
				Tags { "LightMode" = "Always" }
				Cull Front
				ZWrite On
				ColorMask RGB
				Blend SrcAlpha OneMinusSrcAlpha
				//Offset 50,50
	 
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				half4 frag(v2f i) :COLOR { return i.color; }
				ENDCG
			}
		}
	
	 
	

	
	
	Fallback "Toon/Basic"
}
