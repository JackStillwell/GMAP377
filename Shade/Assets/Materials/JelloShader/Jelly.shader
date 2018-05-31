Shader "Custom/Jelly" {
	Properties {
		[Header(color and transparency)]
		_Color ("Color", Color) = (1,1,1,1)
		_RefractAmount("Refract Amount", Range(0,1)) = .13
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Emission ("Emission", Color) = (0,0,0,0)
		[Header(wiggling)]
		_Amount ("Amount", float) = .04
		_ObjectScaleFactor ("Object Scale", float) = 10
		_Speed ("Speed", float) = .5
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }

		GrabPass {
			"_BackGroundTexture"
		}

		Cull Front
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows vertex:vert
			
			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0
						
			sampler2D _BackGroundTexture;
			
			struct Input {
				float4 screenPos : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
				float3 viewDir;
			};
			
			half _Metallic;
			fixed4 _Color;
			fixed4 _Emission;
			half _RefractAmount;
			half _Amount;
			half _ObjectScaleFactor;
			half _Speed;
			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void vert (inout appdata_full v, out Input o) {
      		    UNITY_INITIALIZE_OUTPUT(Input,o);
      		    v.vertex.xz += sin(_Time.w * _Speed + v.vertex.xz * _ObjectScaleFactor) * _Amount;
      		}

			void surf (Input IN, inout SurfaceOutputStandard o) {
				float4 bgcolor = tex2Dproj(_BackGroundTexture, IN.screenPos + half4(IN.worldNormal.x * _RefractAmount, IN.worldNormal.y * _RefractAmount, 0, 0));
				half gray = dot(bgcolor.rgb, half3(.29, 0.58, 0.11));
				half3 grayscale = half3 (gray, gray, gray);
				o.Albedo = grayscale.rgb;
//				o.Metallic = _Metallic;
//				o.Smoothness = _Glossiness;
				o.Alpha = _Color.a;
				o.Emission = _Emission * _Emission.a * (1-dot(-IN.viewDir, IN.worldNormal));
			}
			ENDCG

		Cull Back
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows alpha:fade vertex:vert
		
			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0
		

			struct Input {
				float3 viewDir;
				float3 worldNormal;
			};
		
			half _Metallic;
			half _Glossiness;
			fixed4 _Color;
			fixed4 _Emission;
			half _Amount;
			half _ObjectScaleFactor;
			half _Speed;
		
			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void vert (inout appdata_full v, out Input o) {
      		    UNITY_INITIALIZE_OUTPUT(Input,o);
      		    v.vertex.xz += sin(_Time.w * _Speed + v.vertex.xz * _ObjectScaleFactor) * _Amount;
      		}

		
			void surf (Input IN, inout SurfaceOutputStandard o) {
				// Albedo comes from a texture tinted by color
				o.Albedo = _Color.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = dot(IN.viewDir, IN.worldNormal) * _Color.a;

//				float4 objNormal = mul(unity_WorldToObject, float4(IN.worldNormal, 1));
				o.Emission = dot(IN.viewDir, IN.worldNormal) * _Color;
			}
			ENDCG
	}
	FallBack "Diffuse"
}
