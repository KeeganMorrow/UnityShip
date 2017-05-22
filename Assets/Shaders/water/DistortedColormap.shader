  Shader "Keegan/distorted_colormap" {
    Properties {
        _MainTex ("Pattern", 2D) = "white" {}
        _ColorRamp ("Colour Palette", 2D) = "gray" {}
		_Tess ("Tessellation", Range(1,32)) = 4
		_ampx1 ("X Harmonic 1 Amplitude", float) = 1.0
		_freqx1 ("X Harmonic 1 Frequency", float) = 1.0
		_ampx2 ("X Harmonic 2 Amplitude", float) = 2.3
		_freqx2 ("X Harmonic 2 Frequency", float) = 1.5
		_ampx3 ("X Harmonic 3 Amplitude", float) = 3.3
		_freqx3 ("X Harmonic 3 Frequency", float) = 0.4

		_ampy1 ("Z Harmonic 1 Amplitude", float) = 0.2
		_freqy1 ("Z Harmonic 1 Frequency", float) = 1.8
		_ampy2 ("Z Harmonic 2 Amplitude", float) = 1.8
		_freqy2 ("Z Harmonic 2 Frequency", float) = 1.8
		_ampy3 ("Z Harmonic 3 Amplitude", float) = 2.8
		_freqy3 ("Z Harmonic 3 Frequency", float) = 0.8
		_Phong ("Phong Strengh", Range(0,1)) = 0.5

		_strength ("Strength", float) = 1.0
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
	  LOD 300
      CGPROGRAM
	  #pragma surface surf Lambert tessellate:tessDistance tessphong:_Phong nolightmap
	  #pragma target 4.6
	  #include "Tessellation.cginc"

	  float _Tess;
	  float _Phong;
	  float4 tessDistance (appdata_full v0, appdata_full v1, appdata_full v2) {
		float minDist = 10.0;
		float maxDist = 25.0;
		return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, minDist, maxDist, _Tess);
	  }
      struct Input {
		  float2 uv_MainTex;
          float3 worldPos;
      };

      sampler2D _MainTex;
	  sampler2D _ColorRamp;

	  float _strength;
	  float _ampx1;
	  float _freqx1;
	  float _ampx2;
	  float _freqx2;
	  float _ampx3;
	  float _freqx3;
	  float _ampy1;
	  float _freqy1;
	  float _ampy2;
	  float _freqy2;
	  float _ampy3;
	  float _freqy3;

	  float calculateDistortionX(float y) {
		  float x = 0.0;
	  	  x += sin(y * _ampx1 + _Time * _freqx1);
		  x += sin(y * _ampx2 + _Time * _freqx2);
		  x += sin(y * _ampx3 + _Time * _freqx3);
		  x *= _strength;
		  return x;
	  }
	  float calculateDistortionY(float x) {
		  float y = 0.0;
	  	  y += sin(x * _ampy1 + _Time * _freqy1);
		  y += sin(x * _ampy2 + _Time * _freqy2);
		  y += sin(x * _ampy3 + _Time * _freqy3);
		  y *= _strength;
		  return y;
	  }
      void surf (Input IN, inout SurfaceOutput o) {
	      IN.uv_MainTex.x += calculateDistortionX(IN.worldPos.y);
		  IN.uv_MainTex.y += calculateDistortionY(IN.worldPos.x);
		  float greyscale = tex2D(_MainTex, IN.uv_MainTex).r;
          o.Albedo = tex2D(_ColorRamp, float2(greyscale, 0.5)).rgb;
      }
      ENDCG
    }
 
    Fallback "Diffuse"
  }