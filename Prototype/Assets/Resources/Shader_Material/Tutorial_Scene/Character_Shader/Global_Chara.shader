Shader "CustomCh/Charater/Global_Chara"
{
	Properties
	{
		//Base Diffuse
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[PerRendererData] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0

		//Opaque
		[PerRendererData] _OpaqueMode("Opaque?" , Range(0.0 , 1.0)) = 0
		[PerRendererData] _OpaqueColor("OpaqueColor" , Color) = (1,1,1,1)

		//Dissolve
		_DissolveTexture("Dissolve Texutre", 2D) = "white" {}
		[PerRendererData] _DissolveMode("Dissolve?" , Range(0.0 , 1.0)) = 0
		[PerRendererData] _DissolveAmount("Dissolve Amount", Range(0.0 , 1.0)) = 0
		[PerRendererData] _DissolveEmission("Dissolve Emission", Color) = (1,1,1,1)
		[PerRendererData] _DissolveGrain("Dissolve Grain", Float) = 0

		//Outline
		[PerRendererData] _OutlineMode("Outline?" , Range(0.0 , 1.0)) = 0
		[PerRendererData] _ColorOutline("Color_Outline", Color) = (1, 1, 1, 1)

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
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
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

	float _OpaqueMode;
	half4 _OpaqueColor;

	sampler2D _DissolveTexture;
	half _DissolveAmount;
	float _DissolveMode;
	fixed3 _DissolveEmission;
	half _DissolveGrain;

	half _OutlineMode;
	half4 _ColorOutline;
	float4 _MainTex_TexelSize;

	void surf(Input IN, inout SurfaceOutput o)
	{

		fixed4 c = SampleSpriteTexture(IN.uv_MainTex);
		if (_OpaqueMode == 0)
		{c = c * IN.color;}
		else
		{c = c * _OpaqueColor * 100.0;}

		if (_DissolveMode == 1)
		{
			half dissolve_value = tex2D(_DissolveTexture, IN.uv_MainTex);
			clip(dissolve_value - _DissolveAmount);
			o.Emission = _DissolveEmission * step(dissolve_value - _DissolveAmount, _DissolveGrain) * c.a;
		}

		if (_OutlineMode == 1)
		{
			_ColorOutline.rgb *= _ColorOutline.a;

			fixed alpha_up = c + fixed2(0, _MainTex_TexelSize.y);
			fixed alpha_down = c - fixed2(0, _MainTex_TexelSize.y);
			fixed alpha_right = c + fixed2(_MainTex_TexelSize.x, 0);
			fixed alpha_left = c - fixed2(_MainTex_TexelSize.x, 0);

			c = lerp(c, _ColorOutline, c.a == 0 && alpha_up + alpha_down + alpha_right + alpha_left>0);
		}
		
		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
	}
	ENDCG
	}

		Fallback "Transparent/VertexLit"
}

