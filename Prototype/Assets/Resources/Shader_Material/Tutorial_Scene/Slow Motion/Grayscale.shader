Shader "Custom/Camera/Grayscale"
{

Properties
{
	_MainTex("Base (RGB)", 2D) = "white" {}
	_SaturationAmount("Saturation Amount", Range(-30, 30)) = 1.0
	_BrightnessAmount("Brightness Amount", Range(-30, 30)) = 1.0
	_ContrastAmount("Contrast Amount", Range(-30,30)) = 1.0
	//////////////////////////////////////////////////////////////// DISTORTION
	_DisplacementTex("Pixel Displace", 2D) = "white" {}
	_Strength("Distortion Strength",  Range(-1, 1)) = 0
}
SubShader
{
	Pass
{
	CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

	uniform sampler2D _MainTex;
uniform float _SaturationAmount;
uniform float _BrightnessAmount;
uniform float _ContrastAmount;

float3 ContrastSaturationBrightness(float3 color, float brt, float sat, float con)
{
	//RGB Color Channels
	float AvgLumR = 0.5;
	float AvgLumG = 0.5;
	float AvgLumB = 0.5;

	//Luminace Coefficients for brightness of image
	float3 LuminaceCoeff = float3(0.2125,0.7154,0.0721);

	//Brigntess calculations
	float3 AvgLumin = float3(AvgLumR,AvgLumG,AvgLumB);
	float3 brtColor = color * brt;
	float intensityf = dot(brtColor, LuminaceCoeff);
	float3 intensity = float3(intensityf, intensityf, intensityf);

	//Saturation calculation
	float3 satColor = lerp(intensity, brtColor, sat);

	//Contrast calculations
	float3 conColor = lerp(AvgLumin, satColor, con);

	return conColor;

}

sampler2D _DisplacementTex;
float _Strength;


float4 frag(v2f_img i) : COLOR
{
	// DISTORTION
	half2 n = tex2D(_DisplacementTex, i.uv);
	half2 d = n * 2 - 1;
	i.uv += d * _Strength;
	i.uv = saturate(i.uv);
	// DISTORTION

	float4 renderTex = tex2D(_MainTex, i.uv);

	renderTex.rgb = ContrastSaturationBrightness(renderTex.rgb, _BrightnessAmount, _SaturationAmount, _ContrastAmount);

	return renderTex;

}

ENDCG
}

}
}﻿