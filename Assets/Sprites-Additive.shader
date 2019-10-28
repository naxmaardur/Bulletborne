Shader "Sprites/Additive"
{
	Properties{
		 _Color("Main Color", Color) = (1,1,1,1)
		 _MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		 _AlphaMap("Gradient Transparency Map", 2D) = "white" {}
			_GlowColor("Glow Color", Color) = (1.0, 1.0, 1.0, 1.0)
			_ScrollXSpeed("X Scroll Speed", Float) = 2
			_ScrollYSpeed("Y Scroll Speed", Float) = 2


	}

		SubShader{
			 Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			 LOD 200



		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _AlphaMap;
		float4 _Color;
		fixed4    _GlowColor;
		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;

		struct Input {
			 float2 uv_MainTex;
			 float2 uv_AlphaMap;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			 fixed2 scrolledUV = IN.uv_AlphaMap;
			 fixed xScrollValue = _ScrollXSpeed * _Time;
			 fixed yScrollValue = _ScrollYSpeed * _Time;

			 scrolledUV += fixed2(xScrollValue,yScrollValue);

			 half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			 half4 a = tex2D(_AlphaMap,IN.uv_AlphaMap);
			 c.rgb = _GlowColor;
			 o.Albedo = c.rgb;
			 o.Alpha = c.a * tex2D(_AlphaMap, scrolledUV).r;

		}
		ENDCG
			}


}