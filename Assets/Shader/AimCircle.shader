// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// This two-pass shader renders a silhouette on top of all other geometry when occluded.
Shader "Custom/AimCircle"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" { }
		_SilhouetteColorMult("Silhouette Color Multiplier", Color) = (0.0, 0.0, 0.0, 1.0)
		_Color("Color", Color) = (0.0, 0.0, 0.0, 1.0)
	}

	SubShader
	{
		// Render queue +1 to render after all solid objects
		//Tags { "Queue" = "Geometry+1" "RenderType" = "Transparent" }

		//Offset -1, -1

		Pass
		{
			Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			LOD 100
			ZTest Always
			Offset -1, -1

			// First Pass: Silhouette
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			float4 _Color;
			float4 _SilhouetteColorMult;
			sampler2D _MainTex;

			struct vertInput
			{
				float4 vertex:POSITION;
				float3 normal:NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct fragInput
			{
				float2 uv : TEXCOORD0;
				float4 pos:SV_POSITION;
			};

			fragInput vert(vertInput i)
			{
				fragInput o;
				o.pos = UnityObjectToClipPos(i.vertex);
				o.uv = i.uv;
				return o;
			}

			float4 frag(fragInput i) : COLOR
			{
				return _Color * _SilhouetteColorMult * tex2D(_MainTex, i.uv).a;
			}

			ENDCG
		}
	
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		//LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Offset -1, -1

		// Second Pass: Standard
		CGPROGRAM

		#pragma surface surf Lambert fullforwardshadows finalcolor:color alpha:fade
		#pragma target 3.0

		#include "UnityCG.cginc"

		sampler2D _MainTex;
		float4 _Color;

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			float4 col = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = col.rgb;
			o.Alpha = col.a;
		}

		void color(Input IN, SurfaceOutput o, inout fixed4 col)
		{
			col = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		}

		ENDCG
	}

	FallBack "Diffuse"
}