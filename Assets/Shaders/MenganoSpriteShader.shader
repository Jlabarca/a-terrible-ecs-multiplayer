// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/MenganoSpriteShader"
{

	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
		_MainTex("Sprite", 2D) = "white" {}
		_Flip("Flip", Vector) = (1,1,1,1)
	}
    SubShader
	{
    Tags{ "Queue" = "Transparent"  "RenderType" = "Transparent" }

    Name "FrontPass"

    LOD 500
    Cull Off
    CGPROGRAM
    #pragma target 3.0
    #pragma surface surf SimpleLambert alphatest:_Cutoff vertex:vert addshadow
    #pragma multi_compile_prepassfinal
    #pragma multi_compile_instancing
    #include "UnityCG.cginc"
    sampler2D _MainTex;
    float4 _Flip;

	struct Input {
		float2 uv_MainTex;
	};

	void vert(inout appdata_full v, out Input o)
	{
		UNITY_INITIALIZE_OUTPUT(Input, o);

		// get the camera basis vectors
		float3 forward = -normalize(UNITY_MATRIX_V._m20_m21_m22);
		float3 up = normalize(UNITY_MATRIX_V._m10_m11_m12 * _Flip.y);
		float3 right = normalize(UNITY_MATRIX_V._m00_m01_m02 * _Flip.x);

		// rotate to face camera
		float4x4 rotationMatrix = float4x4(right, 0,
			up, 0,
			forward, 0,
			0, 0, 0, 1);

		//float offset = unity_ObjectToWorld._m22 / 2;
		float offset = 0;
		v.vertex = mul(v.vertex + float4(0, offset, 0, 0), rotationMatrix) + float4(0, -offset, 0, 0);
		v.normal = mul(v.normal, rotationMatrix);
	}

    UNITY_INSTANCING_BUFFER_START(Props)
    UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
    UNITY_DEFINE_INSTANCED_PROP(fixed4, _TeamColor)
    UNITY_INSTANCING_BUFFER_END(Props)
	half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half atten) {
		half4 c;
		c.rgb = s.Albedo * _LightColor0.rgb * (atten) * UNITY_ACCESS_INSTANCED_PROP(Props, _Color) ;
		c.a = s.Alpha;
		return c;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		half4 albedo = tex2D(_MainTex, IN.uv_MainTex) ;
		o.Albedo = albedo.rgb + UNITY_ACCESS_INSTANCED_PROP(Props, _TeamColor); // Emission cancels out other lights
		o.Alpha = albedo.a;
	}
	ENDCG

	}
    FallBack "Standard"
}