// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ToonOutline"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)		//全体のカラー
        _MainTex ("Albedo (RGB)", 2D) = "white" {}		//メインテクスチャー
		_RampTex ("Ramp", 2D) = "white"{}				//光の傾斜テクスチャー

		_Outline("outline", Range(0,0.1)) = 0			//アウトラインの幅
		_OutlineColor("Color",Color) = (0,0,0,1)		//アウトラインの色
    }

    SubShader
    {
		Pass
		{
			Tags { "RemderType"="Opaque" }
			Cull Front

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f 
			{
				float4 pos : SV_POSITION;
			};

			float _Outline;
			float4 _OutlineColor;

			float4 vert(appdata_base v) : SV_POSITION
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);										//
				float3 normal = mul((float3x3) UNITY_MATRIX_MV, v.normal);
				normal.x *= UNITY_MATRIX_P[0][0];
				normal.y *= UNITY_MATRIX_P[1][1];
				o.pos.xy += normal.xy * _Outline;
				return o.pos;
			}

			half4 frag(v2f i) : COLOR
			{
				return _OutlineColor;
			}

			ENDCG
		}
        Tags { "RenderType"="Opaque" }
        LOD 200
		
        CGPROGRAM
        #pragma surface surf ToonRamp
        //#pragma target 3.0

        sampler2D _MainTex;
		sampler2D _RampTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
		
		fixed4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			half d = dot(s.Normal, lightDir) * 0.5 + 0.5;
			fixed3 ramp = tex2D(_RampTex, fixed2(d, 0.5)).rgb;
			fixed4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp;
			c.a = 0;
			return c;
		}

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}