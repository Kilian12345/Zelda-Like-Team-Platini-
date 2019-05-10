Shader "Custom/Outline_Surf"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _ColorOutline("Color_Outline", Color) = (1, 1, 1, 1)
        [PerRendererData] _OutlineMode("Outline?" , Range(0.0 , 1.0)) = 0

    }
    SubShader
    {
        Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest On
		Blend One OneMinusSrcAlpha

		CGPROGRAM
#pragma surface surf Lambert vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
#include "UnitySprites.cginc"

		struct Input
	{
		float2 uv_MainTex;
		fixed4 color;
	};

	void vert(inout appdata_full v, out Input o)
	{
		v.vertex = UnityFlipSprite(v.vertex, _Flip);

#if defined(PIXELSNAP_ON)
		v.vertex = UnityPixelSnap(v.vertex);
#endif

		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.color = v.color * _Color * _RendererColor;
	}

	half _OutlineMode;
	half4 _ColorOutline;
	float4 _MainTex_TexelSize;

	void surf(Input IN, inout SurfaceOutput o)
	{

		fixed4 c = SampleSpriteTexture(IN.uv_MainTex);		

		if (_OutlineMode == 1)
		{
			_ColorOutline.rgb *= _ColorOutline.a;

			fixed alpha_up = tex2D(_MainTex, IN.uv_MainTex + fixed2(0, _MainTex_TexelSize.y)).a;
			fixed alpha_down = tex2D(_MainTex, IN.uv_MainTex - fixed2(0, _MainTex_TexelSize.y)).a;
			fixed alpha_right = tex2D(_MainTex, IN.uv_MainTex + fixed2(_MainTex_TexelSize.x, 0)).a;
			fixed alpha_left = tex2D(_MainTex, IN.uv_MainTex - fixed2(_MainTex_TexelSize.x, 0)).a;

			c = lerp(c, _ColorOutline * 3, c.a == 0 && alpha_up + alpha_down + alpha_right + alpha_left>0);
		}
		
		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
	}
	ENDCG
	}

		Fallback "Transparent/VertexLit"
}
