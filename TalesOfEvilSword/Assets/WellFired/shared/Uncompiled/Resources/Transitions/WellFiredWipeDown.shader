Shader "WellFired/WipeDown" 
{
	Properties 
	{
	    _MainTex ("Texture", 2D) = "white" { }
	    _SecondTex ("Texture", 2D) = "white" { }
	    _Alpha ("float", float) = 0.0
	}
	SubShader 
	{
    	Pass 
    	{
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			sampler2D _SecondTex;
			float _Alpha;
			
			struct v2f 
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
			
			float4 _MainTex_ST;
			
			v2f vert (appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			half4 frag(v2f i) : COLOR
			{
				half4 mainTexcol = tex2D(_MainTex, i.uv);
				half4 secTexcol = tex2D(_SecondTex, i.uv);
				
				half ratio = floor((1.0 - ((_Alpha - i.uv.y) + 0.5)) + 0.5);	
				return (ratio * secTexcol) + ((1.0 - ratio) * ((mainTexcol - secTexcol) + secTexcol));
			}
			ENDCG
		}
	}
	Fallback "VertexLit"
}