Shader "Portal/Masked/Self-Illumin/Bumped Specular Portal 1" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_Illum ("Illumin (A)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_EmissionLM ("Emission (Lightmapper)", Float) = 0
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 400

	Pass {
		Cull Off
		
		CGPROGRAM
		#pragma exclude_renderers gles
		#pragma vertex vert
		#include "UnityCG.cginc"
		
		struct v2f {
			float4 pos : SV_POSITION;
			float4 uv[2] : TEXCOORD0;
			float4 color : COLOR0;
		};
		
		uniform float4x4 _ClipTextureMatrix_Portal_1;
		uniform float4 _Color;
		
		v2f vert( appdata_base v ) {
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			o.uv[0] = v.texcoord;
			
			float4 c = mul( _Object2World, v.vertex );
			c = mul( _ClipTextureMatrix_Portal_1, c );
			o.uv[1] = c;
			//Light
			float4 color = UNITY_LIGHTMODEL_AMBIENT;
			color.xyz += _Color;
			o.color = color;
			return o;
		}
		
		ENDCG

		AlphaTest Greater 0.1
		SetTexture [_MainTex] {
			Combine texture * primary DOUBLE, texture * primary
		}
		SetTexture [_ClipTexture] { Combine previous, texture }
	}    
}
FallBack "Self-Illumin/Specular"
}
