// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SightTileRender" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "black" {}
		_PreFogTex ("Fog (RGB)", 2D) = "white" {}
		_CurFogTex ("Fog (RGB)", 2D) = "white" {}
	}
 
	SubShader 
	{
		ZTest Always 
		Cull Off 
		ZWrite Off 
		Fog { Mode Off }
 
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc" 

			uniform sampler2D _CameraDepthTexture;
			uniform sampler2D _MainTex;

			uniform float4 _MainTex_TexelSize;
			uniform float4x4 _FrustumCornersWS;
			uniform float4 _CameraWS;

			// SightTile Info
			uniform sampler2D _IgnoreTex;
			uniform sampler2D _PreFogTex;
			uniform sampler2D _CurFogTex;

			uniform float _RollSide = 0.0;
			uniform float _OverRollSide = 0.0;
			uniform float _HalfRollSide = 0.0;

			int _iState = 0;
			float _fFloorHeight = 0.0;
			float _fLrepState = 0;
			float _fLrepFogTex = 0;
			

			struct v2f 
			{
				float4 pos : POSITION;
				half2 uv : TEXCOORD0;
				float2 uv_depth : TEXCOORD1;
				float4 interpolatedRay : TEXCOORD2;
			};

			//
			//
   
			v2f vert( appdata_img v )
			{
				v2f o;
				half index = v.vertex.z;
				v.vertex.z = 0.1;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy;
				o.uv_depth = v.texcoord.xy;
		
				#if UNITY_UV_STARTS_AT_TOP
				if (_MainTex_TexelSize.y < 0)
					o.uv.y = 1-o.uv.y;
				#endif

				o.interpolatedRay = _FrustumCornersWS[(int)index];
				o.interpolatedRay.w = index;
		
				return o;
			}
    
			//
			//

			fixed4 frag (v2f i) : COLOR 
			{
				// Get WorldSpace Pixel Position
				float rawdpth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv_depth);
				float dpth = Linear01Depth(rawdpth);
				float4 wsDir = dpth * i.interpolatedRay;
				float4 wsPos = _CameraWS + wsDir;

				fixed4 orgCol = tex2D(_MainTex, i.uv);
				fixed4 col = orgCol;
				fixed4 fogCol;

				if (_iState == 0)
					return orgCol;

				if (wsPos.x < 0 || wsPos.z < 0 || wsPos.x > 250 || wsPos.z > 250 || wsPos.y < -1.0f)
				{
					//col = col * fixed4(0.09f, 0.09f, 0.09f, 0.5f);
					//col = fixed4(0.0f, 0.0f, 0.0f, 0.0f);
					//col = fixed4(1.0f, 1.0f, 1.0f, 1.0f);
					col = col;
				}
				else
				{
					uint idxX5 = (uint)(wsPos.x * 5);
					uint idxZ5 = (uint)(wsPos.z * 5);
					float beginIdxTx5 = (float)(idxX5 % 50);
					float beginIdxTz5 = (float)(idxZ5 % 50);
					float2 puv;
					puv.x = (beginIdxTx5)*0.02f;
					puv.y = (beginIdxTz5)*0.02f;
					fixed4 igTex = tex2D(_IgnoreTex, puv);
					if(igTex.x <0.5f)
						col = col;
					else
					{
						// World Pos => Texture UV
						uint idxX = (uint)(wsPos.x * _OverRollSide);
						uint idxZ = (uint)(wsPos.z * _OverRollSide);

						float beginIdxTx = (float)(idxX % 3);
						float beginIdxTz = (float)(idxZ % 3);
						float lastTx = (wsPos.x - idxX * _RollSide)*_OverRollSide;
						float lastTz = (wsPos.z - idxZ * _RollSide)*_OverRollSide;

						float2 puv;
						puv.x = (beginIdxTx + lastTx)*0.333333f;
						puv.y = (beginIdxTz + lastTz)*0.333333f;
						fixed4 preTex = tex2D(_PreFogTex, puv);
						fixed4 curtex = tex2D(_CurFogTex, puv);

						col = col * ((preTex + (curtex - preTex) * _fLrepFogTex));
					}
				}

				if (_iState == 1) // FogTile
					col *= step(rawdpth, 0.999); // don't show fog on the far plane

				return col;

				if(_fLrepState < 0)
				{
					float avg = (orgCol.r + orgCol.g + orgCol.b) ; //  1/7
					fixed4 grayCol = fixed4(avg, avg, avg, 1);
					col = fogCol + (fogCol - grayCol)*_fLrepState; // lrep 0 -> origin, 1 -> gray
				}
				else
				{

					col = fogCol + (orgCol - fogCol)*_fLrepState; // lrep 0 -> origin, 1 -> gray
				}


				return col;
			}
			ENDCG
		}
	} 

	FallBack off
}