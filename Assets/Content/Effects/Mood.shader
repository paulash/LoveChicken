Shader "Custom/Mood"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_ColorDelta ("Color Variation", Color) = (0.1, 0.1, 0.1, 0.0)

		_WIntensityC ("Color Wave Intensity", Float) = 1.0
		_WFrequencyC ("Color Wave Frequency", Float) = 1.0

		_HSpeed ("Horizontal Speed", Float) = 1.0
		_WIntensity ("Wave Intensity", Float) = 1.0
		_WSpeed ("Wave Speed", Float) = 1.0
		_WFrequency ("Wave Frequency", Float) = 1.0
		_Mood ("Mood", Int) = 0
    }
    SubShader
    {
        Tags 
		{ 
			"Queue" = "Transparent" 
			"RenderType" = "Transparent" 
			"IgnoreProjector" = "True"
			"PreviewType" = "Plane"
		}

		Cull Off
		Lighting Off
		ZTest Off
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#include "UnityCG.cginc"

			// input information
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
            };

            sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			float _HSpeed;
			float _WIntensity;
			float _WSpeed;
			float _WFrequency;
			
			float _WIntensityC;
			float _WFrequencyC;
			fixed4 _ColorDelta;

            v2f vert (appdata v)
            {
                v2f o;
				// move vertex up a bit
				o.vertex = UnityObjectToClipPos(v.vertex);
				v.uv.x += _Time[1] * _HSpeed;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = _Color + sin(_Time[1] * _WFrequencyC) * _WIntensityC * _ColorDelta;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				i.uv.y += sin((i.uv.x + _Time[1] * _WSpeed) * _WFrequency) * _WIntensity;
				half4 col = tex2D(_MainTex, i.uv) * i.color;
                return col;
            }
            ENDCG
        }
    }
}
