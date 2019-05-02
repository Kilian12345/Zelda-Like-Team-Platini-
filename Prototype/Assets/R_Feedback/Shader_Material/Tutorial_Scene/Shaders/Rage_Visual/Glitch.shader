Shader "Unlit/Glitch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        //Random1
        _ROneSin ("R1Sin", Float) = 127.1
        _ROneFract ("R1Fract", Float) = 437.53

        //Random3
        _RThreeSinX ("R3Sin.x", Float) = 127.1
        _RThreeSinY ("R3Sin.y", Float) = 311.7
        _RThreeSinW ("R3Sin.w", Float) = 231.4
        _RThreeFract ("R3Fract", Float) = 437.53

        //Function
        _ValueOne ("_ValueOne Traits/Grain", Float) = 1.0
        _ValueTwo ("_ValueTwo puissance", Float) = 100
        _ValueThree ("_ValueThree pow", Float) = 5
        _ValueFour ("_ValueFour etirement", Float) = 1
        _ValueFive ("_ValueFive frequence", Float) = -30.5
        _ValueSix ("_ValueSix time", Float) = 0.0001
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"


            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _ROneSin ;
            float _ROneFract ;

            //Random3
            float _RThreeSinX ;
            float _RThreeSinY ;
            float _RThreeSinW ;
            float _RThreeFract ;

            //Function
            float _ValueOne ;
            float _ValueTwo ;
            float _ValueThree;
            float _ValueFour ;
            float _ValueFive ;
            float _ValueSix ;



            float random1(float p) { return frac(sin(p * _ROneSin) * _ROneFract) * 1.0 - 0.0; }
			float random3(float3 p) {return frac(sin(dot(p, float3(_RThreeSinX, _RThreeSinY, _RThreeSinW))) * _RThreeFract) * 2.0 - 1.0;}
 
            fixed4 frag (v2f_img i) : COLOR
            {
            
                // sample the texture
                float2 p = float2(i.uv.x , -i.uv.y);
				p.x += lerp(random3(float3(p.x - fmod(p.x + _ValueOne,random1(p.y * _ValueTwo)), p.y, pow(p.y,_ValueThree))), 0.0, _ValueFour + exp(_ValueFive * random1(_Time - fmod(_Time, _ValueSix))));
                //p.x += lerp(random3(float3(p.x - fmod(p.x + 1.0,random1(p.y * 100)), p.y, pow(p.y,5))), 0.0, 1 + exp(-30.5 * random1(_Time - fmod(_Time, 0.0001))));
				float3 color = tex2D(_MainTex, float2(p.x, -p.y)).rgb;
                float3 baseColor = tex2D(_MainTex, i.uv);

                return float4(color, 1.0);
            }
            ENDCG
        }
    }
    Fallback off
}


