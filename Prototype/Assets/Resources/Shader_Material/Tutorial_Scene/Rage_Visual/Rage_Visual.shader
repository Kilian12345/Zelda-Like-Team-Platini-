Shader "Camera/Rage_Visual"
{
   Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_OffsetColor("Offset Color", Range(-0.05, 0.05)) = 0

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

    fixed4 _BaseTexture;
    fixed4 _ModifyTexture;

	int _Which;
	half4 _One;
	half4 _Two;
	half4 _Three;
	half4 _Four;
	half4 _Five;
	half4 _Six;
	half4 _Seven;
	half4 _FinalColor;

	fixed4 frag(v2f i) : SV_Target
	{
        //Screen Wave
	//i.uv = float2(frac(i.uv.x + cos((i.uv.y + _CosTime.y) * 100) / _OffsetDistortion), frac(i.uv.y + _OffsetPosY));

    float2 screenPos = i.screenPos.xy / i.screenPos.w;
    screenPos.y *= _ScreenParams.y / _ScreenParams.x;
    fixed dist = sqrt(dot(screenPos,screenPos));

    _Scale = 
    (-0.6037*pow(_OffsetColor,6)) + (84.239*pow(_OffsetColor,5))
    +(12.985*pow(_OffsetColor,4))+(6.2425*pow(_OffsetColor,3))
    + (4.0693*pow(_OffsetColor,2)) + (2.0001 * _OffsetColor)
    + 1.0002;
    

	fixed4 col = tex2D(_MainTex, i.uv);
    fixed4 col2 = tex2D(_MainTex, i.uv);
	col = (tex2D(_MainTex, (i.uv / _Scale) + _OffsetColor ));
    _BaseTexture = col2;
    _ModifyTexture = col;

	//Different Color
	_One = half4 (_ModifyTexture.r, _BaseTexture.g, _BaseTexture.b, 1);
	_Two = half4 (_BaseTexture.r, _ModifyTexture.g, _BaseTexture.b, 1);
	_Three = half4 (_BaseTexture.r, _BaseTexture.g, _ModifyTexture.b, 1);
	_Four = half4 (_ModifyTexture.r, _ModifyTexture.g, _BaseTexture.b, 1);
	_Five = half4 (_BaseTexture.r, _ModifyTexture.g, _ModifyTexture.b, 1);
	_Six = half4 (_ModifyTexture.r, _BaseTexture.g, _ModifyTexture.b, 1);
	_Seven = half4 (_BaseTexture.r, _BaseTexture.g, _BaseTexture.b, 1);

	if (_Which == 0) {_FinalColor = _Seven;}
	if (_Which == 1) { _FinalColor = _One; }
	if (_Which == 2) { _FinalColor = _Two; }
	if (_Which == 3) { _FinalColor = _Three; }
	if (_Which == 4) { _FinalColor = _Four; }
	if (_Which == 5) { _FinalColor = _Five; }
	if (_Which == 6) { _FinalColor = _Six; }

	//_FinalColor = _Two;

    return _FinalColor;
	}
		ENDCG
	}
    }
}
