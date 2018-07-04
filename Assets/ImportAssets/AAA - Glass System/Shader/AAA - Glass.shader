// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Cutie Cube/Filters/AAA - Glass" 
{
	Properties 
	{
		_GlassTex ("Glass texture", 2D) = "white" {}	
		_GlassTint ("Glass tint", Color) = (1,1,1,1)	
		_Transparency ("Transparency", Range(0,1)) = 0
		_Normal ("Normal", 2D) = "normal" {}	
		_RefZoom ("Zoom", Range(0.34,1)) = 0.6
		_CutOut ("Cut out", Range(0,0.5)) = 0
		_Smooth ("Smooth", Range(0.001,0.05)) = 0
		[HideInInspector] _MainTex ("", 2D) = "white" {}
	}
 
	SubShader 
	{
 
		ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings
 
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag alpha
			#include "UnityCG.cginc" 
			//we include "UnityCG.cginc" to use the appdata_img struct 
    
			struct v2f 
			{
				float4 pos : POSITION;
				half2 uv : TEXCOORD0;
				half2 uv1 : TEXCOORD1;
			};
   			    
			sampler2D _MainTex;
			sampler2D _Normal;
			sampler2D _GlassTex;
			fixed4 _GlassTint;
			fixed _Transparency;
			fixed _RefZoom;
			fixed _CutOut;
			fixed _Smooth;

			//Our Vertex Shader 

			struct vertexInput
			{
				float4 vertex : POSITION; 
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
			};

			v2f vert (vertexInput v)
			{
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f,o)
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);

				//v.texcoord1.x += frac(sin(_Time/ cos(_Time +v.vertex.x)  +v.vertex.x));
				//v.texcoord1.y += frac(sin(0.6*_Time+v.vertex.z));
				//o.uv1 = v.texcoord1.xy;
				return o; 
			}

    
			//Our Fragment Shader
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 orgCol;
				fixed gNormalNormilize = normalize(tex2D(_Normal, i.uv));
				gNormalNormilize = (int)(gNormalNormilize/_Smooth) * _Smooth;
				if(gNormalNormilize > _CutOut)
					orgCol = tex2D(_MainTex, i.uv * gNormalNormilize / _RefZoom + gNormalNormilize * _RefZoom * _RefZoom + _Smooth) * lerp(tex2D(_GlassTex, i.uv) * _GlassTint, fixed4(1,1,1,1), _Transparency); //Get the orginal rendered color 
				else
					orgCol = tex2D(_MainTex, i.uv);
				return orgCol;				
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}