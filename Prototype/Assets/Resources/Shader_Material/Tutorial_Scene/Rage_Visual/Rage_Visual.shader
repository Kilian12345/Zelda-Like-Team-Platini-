﻿Shader "Camera/Rage_Visual"
{
   Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_OffsetColor("Offset Color", Float) = 0
        _Scale("Scale",Range(0.005, 2)) = 0

	}
		SubShader
	{
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		struct v2f
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
        float4 screenPos : TEXCOORD2;
	};



	half _OffsetNoiseX;
	half _OffsetNoiseY;

	v2f vert(appdata_base v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
        o.screenPos = o.pos;
		o.uv = v.texcoord;
		return o;
	}

	sampler2D _MainTex;
	sampler2D _SecondaryTex;

	float _OffsetColor;
    float _Scale;
	half _OffsetPosY;
	half _OffsetDistortion;

	fixed4 frag(v2f i) : SV_Target
	{
        //Screen Wave
	//i.uv = float2(frac(i.uv.x + cos((i.uv.y + _CosTime.y) * 100) / _OffsetDistortion), frac(i.uv.y + _OffsetPosY));

    float2 screenPos = i.screenPos.xy / i.screenPos.w;
    screenPos.y *= _ScreenParams.y / _ScreenParams.x;
    fixed dist = sqrt(dot(screenPos,screenPos));
    
	fixed4 col = tex2D(_MainTex, i.uv);
	col.r = (tex2D(_MainTex, (i.uv / _Scale) + _OffsetColor )).r;
    //col.r = (tex2D(_MainTex, i.uv * _Scale)).r ;
	//col.b = tex2D(_MainTex, i.uv + float2(-_OffsetColor, -_OffsetColor)).b;


    return col;
	}
		ENDCG
	}
    }
}
