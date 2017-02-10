// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "Hedgehog Team/Vegetation-lightmap" {
Properties {
	_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	_SecondaryFactor ("Factor for up and fown bending", float) = 2.5
}

SubShader {
	Tags {"Queue"="AlphaTest" "RenderType"="TransparentCutout" "LightMode"="ForwardBase"}
	LOD 100
	cull off
	AlphaTest Greater [_Cutoff]
	
	CGINCLUDE
	#include "UnityCG.cginc"
	#include "TerrainEngine.cginc"
	sampler2D _MainTex;
	half4 _MainTex_ST;
	
	fixed _Cutoff;
	
	#ifndef LIGHTMAP_OFF
	// float4 unity_LightmapST;
	// sampler2D unity_Lightmap;
	#endif
	
	float _SecondaryFactor;
	
	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		#ifndef LIGHTMAP_OFF
		float2 lmap : TEXCOORD1;
		#endif
		fixed3 spec : TEXCOORD2;
		
		fixed4 color : COLOR;
	};

inline float4 AnimateVertex2(float4 pos, float3 normal, float4 animParams, float SecondaryFactor)
{	

	float fDetailAmp = 0.1f;
	float fBranchAmp = 0.3f;
	
	// Phases (object, vertex, branch)
	float fObjPhase = dot(unity_ObjectToWorld[3].xyz, 1);
	
	float fBranchPhase = fObjPhase;// + animParams.x;
	float fVtxPhase = dot(pos.xyz, animParams.y + fBranchPhase);
	
	// x is used for edges; y is used for branches
	// use pos.xz to create some variation
	float2 vWavesIn = _Time.yy + pos.xz *.3 + float2(fVtxPhase, fBranchPhase );
	
	// 1.975, 0.793, 0.375, 0.193 are good frequencies
	float4 vWaves = (frac( vWavesIn.xxyy * float4(1.975, 0.793, 0.375, 0.193) ) * 2.0 - 1.0);
	vWaves = SmoothTriangleWave( vWaves );
	float2 vWavesSum = vWaves.xz + vWaves.yw;

	// Edge (xz) and branch bending (y)
	// sign important to match normals of both faces!!! otherwise edge fluttering will be corrupted.
	float3 bend = animParams.y * fDetailAmp * normal.xyz * sign(normal.xyz);
	
	bend.y = animParams.z * fBranchAmp * SecondaryFactor; // controlled by vertex color red
	pos.xyz += ((vWavesSum.xyx * bend) + (_Wind.xyz * vWavesSum.y * animParams.w)) * _Wind.w; 

	// Primary bending
	// Displace position
	pos.xyz += animParams.w * _Wind.xyz * _Wind.w; // controlled by vertex color blue
	
	return pos;
}

	v2f vert (appdata_full v)
	{
		v2f o;
		float4	windParams	= float4(0, v.color.g, v.color.r, v.color.b);		
		// call vertex animation
		float4 mdlPos = AnimateVertex2(v.vertex, v.normal, windParams, _SecondaryFactor);
		o.pos = mul(UNITY_MATRIX_MVP,mdlPos);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		//o.spec = v.color;
		#ifndef LIGHTMAP_OFF
		o.lmap = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
		#endif
		
		o.color.rgba = v.color.rgba;
		
		return o;
	}
	ENDCG

	Pass {
		CGPROGRAM
		#pragma debug
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest		
		fixed4 frag (v2f i) : COLOR
		{
			fixed4 tex = tex2D (_MainTex, i.uv);
			clip(tex.a - _Cutoff);
			fixed4 c;
			c.rgb = tex.rgb * i.color.a;
			c.a = tex.a;
			
			#ifndef LIGHTMAP_OFF
			fixed3 lm = DecodeLightmap (UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lmap));
			c.rgb *= lm;
			#endif
			
			return c;
		}
		ENDCG 
	}	
}
}
