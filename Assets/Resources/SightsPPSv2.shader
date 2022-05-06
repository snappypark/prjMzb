Shader "Hidden/SightsPPSv2"
{
	CGINCLUDE

	#include "UnityCG.cginc"

	uniform float _fLrepFogTex;
	uniform float _MainY;
	uniform int _iState;
	//uniform sampler2D _IgnoreTex;
	uniform sampler2D _PreFogTex;
	uniform sampler2D _CurFogTex;

	uniform float _RollSide;
	uniform float _OverRollSide;
	uniform float _HalfRollSide;

	uniform sampler2D _MainTex;
	uniform sampler2D_float _CameraDepthTexture;

	// for fast world space reconstruction
	uniform float4x4 _InverseView;

	struct v2f
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		float3 interpolatedRay : TEXCOORD1;
	};

	v2f vert(float4 vertex : POSITION)
    {
        float far = _ProjectionParams.z;
        float2 orthoSize = unity_OrthoParams.xy;
        float isOrtho = unity_OrthoParams.w; // 0: perspective, 1: orthographic

        // Vertex pos -> clip space vertex position
        float3 viewpos = float3(vertex.xy * 2 - 1, 1);

        // Perspective: view space vertex position of the far plane
        float3 rayPers = mul(unity_CameraInvProjection, viewpos.xyzz * far).xyz;

        // Orthographic: view space vertex position
        float3 rayOrtho = float3(orthoSize * viewpos.xy, 0);

        v2f o;
        o.pos = float4(viewpos.x, -viewpos.y, 1, 1);
        o.uv = (viewpos.xy + 1) * 0.5;
        o.interpolatedRay = lerp(rayPers, rayOrtho, isOrtho);
		
		//#if UNITY_UV_STARTS_AT_TOP // Unity's documentation says to do this, but it seems to cause issues with some graphics APIs
		if (_ProjectionParams.x > 0)
			o.pos.y = -o.pos.y;
		//#endif
		
        return o;
    }

	ENDCG

	SubShader
	{
		ZTest Always
		Cull Off
		ZWrite Off
		ZTest Always
		Fog { Mode Off }

		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"
			#include "HLSLSupport.cginc"

			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile CAMERA_ORTHOGRAPHIC CAMERA_PERSPECTIVE
			#pragma multi_compile PLANE_XY PLANE_YZ PLANE_XZ
			#pragma multi_compile FOG_COLORED FOG_TEXTURED_WORLD FOG_TEXTURED_SCREEN
			#pragma multi_compile _ FOGFARPLANE
			#pragma multi_compile _ FOGOUTSIDEAREA
			
			float3 ComputeViewSpacePosition(float2 texcoord, float3 ray, out float rawdpth)
			{
				// Render settings
				float near = _ProjectionParams.y;
				float far = _ProjectionParams.z;
				float isOrtho = unity_OrthoParams.w; // 0: perspective, 1: orthographic

				// Z buffer sample
				float z = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, texcoord);

				// Far plane exclusion
				#if !defined(EXCLUDE_FAR_PLANE)
				float mask = 1;
				#elif defined(UNITY_REVERSED_Z)
				float mask = z > 0;
				#else
				float mask = z < 1;
				#endif

				// Perspective: view space position = ray * depth
				rawdpth = Linear01Depth(z);
				float3 vposPers = ray * rawdpth;

				// Orthographic: linear depth (with reverse-Z support)
				#if defined(UNITY_REVERSED_Z)
				float depthOrtho = -lerp(far, near, z);
				#else
				float depthOrtho = -lerp(near, far, z);
				#endif

				// Orthographic: view space position
				float3 vposOrtho = float3(ray.xy, depthOrtho);

				// Result: view space position
				return lerp(vposPers, vposOrtho, isOrtho) * mask;
			}

			half4 frag (v2f i) : SV_Target
			{
				// for VR
				i.uv = UnityStereoTransformScreenSpaceTex(i.uv);

				float rawdpth;
				float3 viewspacepos = ComputeViewSpacePosition(i.uv, i.interpolatedRay, rawdpth);
				float3 wsPos = mul(_InverseView, float4(viewspacepos, 1)).xyz;

				// single pass VR requires the world space separation between eyes to be manually set
				#if UNITY_SINGLE_PASS_STEREO
				wsPos.x += unity_StereoEyeIndex * _StereoSeparation;
				#endif

				//return fixed4(0.1f, 0.1f, 0.1f, 1.0f);

				fixed4 orgCol = tex2D(_MainTex, i.uv);
				fixed4 col = orgCol;
				//if (_iState == 0)
				//	return orgCol;
				//col = fixed4(0.1f, 0.1f, 0.1f, 1.0f);

				if (wsPos.x < 0 || wsPos.z < 0 ||
					wsPos.x > 300 || wsPos.z > 300 || wsPos.y < -1.0f)
				{
					//col = col * fixed4(0.41f, 0.429f, 0.432f, 0.5f);
					col = col * fixed4(0.77f, 0.77f, 0.77f, 1.0f);
					//col = col * fixed4(0.84f, 0.84f, 0.84f, 1.0f);
					//return fixed4(0, 0, 0, 0);
				}
				else
				{
					if(wsPos.y < _MainY - 3)
						//return col*fixed4(0.21f, 0.21f, 0.21f, 1);
						return fixed4(0, 0, 0, 0);
					uint iX = (uint)wsPos.x;
					uint iZ = (uint)wsPos.z;
					float2 puv1 = float2(((iX % 48) + wsPos.x - iX)*0.020833333333f,
						((iZ % 48) + wsPos.z - iZ)*0.020833333333f);
					float4 preTex = tex2D(_PreFogTex, puv1);
					float4 curtex = tex2D(_CurFogTex, puv1);

					col = col * ((preTex + (curtex - preTex) * _fLrepFogTex));
				}

				/*
				if (_iState == 6) // FogTile
				{
					if (rawdpth == 0) // there's some weird behaviour with the far plane in some rare cases that this should fix...
						col = 0;
					else
						col *= step(rawdpth, 0.999); // don't show fog on the far plane
				}*/

				return col;
			}

			ENDCG
		}
	}

	Fallback off
}
