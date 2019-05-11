// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Explosion_Amp"
{
	Properties
	{
		_Fractmultiply("Fract multiply", Range( 0 , 40)) = 2
		_St_Multiply("St_Multiply", Float) = 2
		_Repeat("Repeat", Float) = 0
		_VectroTest("Vectro Test", Vector) = (0,0,0,0)
		_RayColor("Ray Color", Color) = (1,0,0,0.9921569)
		_Dissolve("Dissolve", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _RayColor;
		uniform float _St_Multiply;
		uniform float3 _VectroTest;
		uniform float _Repeat;
		uniform float _Fractmultiply;
		uniform float _Dissolve;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = float4(1,0.01810108,0,1).rgb;
			float3 St51 = ( float3( ( 1.0 - ( ( ( i.uv_texcoord / float2( 1,1 ) ) * ( _ScreenParams.y / _ScreenParams.x ) ) * _St_Multiply ) ) ,  0.0 ) + _VectroTest );
			float mulTime162 = _Time.y * _Repeat;
			float D60 = length( ( abs( St51 ) + mulTime162 ) );
			float temp_output_62_0 = frac( ( D60 * ( 1.0 * _Fractmultiply ) ) );
			o.Emission = ( _RayColor * temp_output_62_0 ).rgb;
			o.Alpha = ( temp_output_62_0 + (0.0 + (( 1.0 - _Dissolve ) - 0.0) * (1.0 - 0.0) / (1.0 - 0.0)) );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows exclude_path:deferred 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15900
0;617;1128;383;947.4786;-276.5159;1.3;True;False
Node;AmplifyShaderEditor.CommentaryNode;40;-1931.864,-80.0276;Float;False;1786.89;811.7072;CIRCLE;12;51;78;50;79;48;49;44;47;41;129;130;156;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScreenParams;129;-1782.741,394.3914;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenParams;130;-1786.337,561.6125;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexCoordVertexDataNode;41;-1803.864,31.97231;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;47;-1533.49,508.2928;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;156;-1510.699,193.8471;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-1312.71,400.7034;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-939.8641,447.9731;Float;False;Property;_St_Multiply;St_Multiply;1;0;Create;True;0;0;False;0;2;0.57;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-923.8641,351.973;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;50;-763.8642,351.973;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector3Node;79;-827.8642,559.973;Float;False;Property;_VectroTest;Vectro Test;3;0;Create;True;0;0;False;0;0,0,0;-0.8396,-0.84,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;78;-635.8643,463.9729;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;52;-1803.864,783.973;Float;False;1035.619;635.0852;D;7;58;60;54;57;55;56;162;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;51;-459.8644,351.973;Float;False;St;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;56;-1771.864,991.9731;Float;False;51;St;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-1737.127,1162.549;Float;False;Property;_Repeat;Repeat;2;0;Create;True;0;0;False;0;0;-0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;55;-1563.864,1007.973;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleTimeNode;162;-1734.086,1254.678;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;57;-1371.864,1039.973;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LengthOpNode;54;-1211.864,1039.973;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-115.105,655.2709;Float;False;Property;_Fractmultiply;Fract multiply;0;0;Create;True;0;0;False;0;2;365;0;40;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;60;-1019.864,1039.973;Float;False;D;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;193;-11.91125,527.5831;Float;False;2;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;131;511.0646,890.2708;Float;False;Property;_Dissolve;Dissolve;5;0;Create;True;0;0;False;0;0;0.44;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;63;-183.2805,312.368;Float;False;60;D;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;132;509.7571,807.1874;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;7.970938,342.8327;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;81;59.41614,33.05204;Float;False;Property;_RayColor;Ray Color;4;0;Create;True;0;0;False;0;1,0,0,0.9921569;1,0,0.06448603,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;133;514.5082,630.0434;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;166;-1907.028,-719.3104;Float;False;1911.942;531.1748;Pixelisation;13;188;167;168;171;184;170;180;179;182;175;176;178;177;;1,1,1,1;0;0
Node;AmplifyShaderEditor.FractNode;62;186.0103,369.5511;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;178;-1150.389,-394.8211;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;179;-1014.98,-435.8292;Float;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;308.0262,161.9568;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;176;-1390.528,-302.6626;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;184;-636.6484,-528.4122;Float;True;Property;_TextureSample0;Texture Sample 0;8;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;188;-262.272,-481.6927;Float;False;Pixelize;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;171;-1679.082,-423.8876;Float;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;180;-1013.573,-538.6807;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;170;-1680.246,-554.0289;Float;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;175;-1384.608,-687.5493;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;168;-1874.449,-408.9688;Float;False;Property;_PixelNumberY;_PixelNumberY;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;134;538.9148,506.7002;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;177;-1143.131,-582.3608;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;190;738.2735,-110.1873;Float;False;188;Pixelize;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;167;-1879.978,-538.4298;Float;False;Property;_PixelNumberX;_PixelNumberX;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;148;393.3031,-86.38938;Float;False;Constant;_Color0;Color 0;5;0;Create;True;0;0;False;0;1,0.01810108,0,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;182;-834.7849,-503.3798;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1087.05,170.9022;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Explosion_Amp;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;47;0;129;2
WireConnection;47;1;130;1
WireConnection;156;0;41;0
WireConnection;44;0;156;0
WireConnection;44;1;47;0
WireConnection;48;0;44;0
WireConnection;48;1;49;0
WireConnection;50;0;48;0
WireConnection;78;0;50;0
WireConnection;78;1;79;0
WireConnection;51;0;78;0
WireConnection;55;0;56;0
WireConnection;162;0;58;0
WireConnection;57;0;55;0
WireConnection;57;1;162;0
WireConnection;54;0;57;0
WireConnection;60;0;54;0
WireConnection;193;1;64;0
WireConnection;132;0;131;0
WireConnection;65;0;63;0
WireConnection;65;1;193;0
WireConnection;133;0;132;0
WireConnection;62;0;65;0
WireConnection;178;0;176;0
WireConnection;178;1;171;0
WireConnection;179;0;171;0
WireConnection;179;1;178;0
WireConnection;80;0;81;0
WireConnection;80;1;62;0
WireConnection;184;1;182;0
WireConnection;188;0;184;0
WireConnection;171;1;168;0
WireConnection;180;0;177;0
WireConnection;170;1;167;0
WireConnection;134;0;62;0
WireConnection;134;1;133;0
WireConnection;177;0;175;0
WireConnection;177;1;170;0
WireConnection;182;0;180;0
WireConnection;182;1;179;0
WireConnection;0;0;148;0
WireConnection;0;2;80;0
WireConnection;0;9;134;0
ASEEND*/
//CHKSM=33F219D3992157BABE2084867A55BC6E5E5BB53F