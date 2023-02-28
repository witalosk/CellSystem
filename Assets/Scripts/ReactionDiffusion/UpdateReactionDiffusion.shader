Shader "Hidden/UpdateReactionDiffusion"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "Assets/ShaderCommon/Common.hlsl"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float _Du;
			float _Dv;
			float _F;
			float _K;
			
			sampler2D _MainTex;				// Input
			float4 _MainTex_TexelSize;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = TransformObjectToHClip(v.vertex);
				o.uv = v.uv;
				return o;
			}
			

			float4 frag (v2f i) : SV_Target
			{
				const float2 this = tex2D(_MainTex, i.uv).xy;
				const float2 up = tex2D(_MainTex, i.uv + float2(0, _MainTex_TexelSize.y)).xy;
				const float2 down = tex2D(_MainTex, i.uv - float2(0, _MainTex_TexelSize.y)).xy;
				const float2 left = tex2D(_MainTex, i.uv - float2(_MainTex_TexelSize.x, 0)).xy;
				const float2 right = tex2D(_MainTex, i.uv + float2(_MainTex_TexelSize.x, 0)).xy;

				float uSpeed = _Du - this.x * this.y * this.y + _F * (1.0 - this.x);
				float vSpeed = _Dv + this.x * this.y * this.y - this.y * (_F + _K);
				
				
				return float4(this.x + uSpeed * unity_DeltaTime.x, this.y + vSpeed * unity_DeltaTime.x, 0.0, 0.0);
			}
			ENDHLSL
		}
	}
}