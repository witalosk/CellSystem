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

			float _DiffusionU;	// 1.0
			float _DiffusionV;	// 0.5
			float _Feed;	// 0.037
			float _Kill;	// 0.062

			float3 _MousePosition;
			
			sampler2D _MainTex;				// Input
			float4 _MainTex_TexelSize;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = TransformObjectToHClip(v.vertex.xyz);
				o.uv = v.uv;
				return o;
			}
			

			float4 frag (v2f i) : SV_Target
			{
				const float2 this = tex2D(_MainTex, i.uv).xy;
				float2 lap = Laplacian(_MainTex, _MainTex_TexelSize.xy, i.uv).xy;

				if (_MousePosition.z > 0.5 && distance(_MousePosition.xy, i.uv) < 0.05)
				{
					_Feed = 0.0;
				}

				float uvv = this.x * this.y * this.y;
				float uSpeed = _DiffusionU * lap.x - uvv + _Feed * (1.0 - this.x);
				float vSpeed = _DiffusionV * lap.y + uvv - this.y * (_Feed + _Kill);
				float dt = 1.0;

				return float4(this.x + uSpeed * dt, this.y + vSpeed * dt, 0.0, 0.0);
			}
			ENDHLSL
		}
	}
}