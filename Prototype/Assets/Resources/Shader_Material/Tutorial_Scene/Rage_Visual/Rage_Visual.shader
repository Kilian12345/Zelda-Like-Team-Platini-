Shader "Camera/Rage_Visual"
{
   Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_OffsetColor("Offset Color", Range(-0.05, 0.05)) = 0
        [HideInInspector]_R ("R" , Float) = 0
        [HideInInspector]_G ("G" , Float) = 0
        [HideInInspector]_B ("B" , Float) = 0
            [HideInInspector]_BaseTexture ("Base Tex" , Float) = 0
            [HideInInspector]_ModifyTexture ("Modify Tex" , Float) = 0

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

    fixed3 _R;
    fixed3 _G;
    fixed3 _B;
    fixed4 _BaseTexture;
    fixed _ModifyTexture;

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

    return half4(col.r , col2.g, col2.b, 1);
	}
		ENDCG
	}
    }
}
