// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "YesilayCharacter"
{
	Properties
	{
		_Base2d("Base2d", 2D) = "white" {}
		_Normal2d("Normal2d", 2D) = "bump" {}
		_Mask2d("Mask2d", 2D) = "white" {}
		_Brightbess("Brightbess", Range( 0 , 5)) = 1
		_Smoothness("Smoothness", Range( 0 , 1)) = 1
		_Specular("Specular", Range( 0 , 1)) = 0.2
		_Speculartint("Specular tint", Range( 0 , 1)) = 0.5
		_Aoonbasecolor("Ao on basecolor", Range( 0 , 1)) = 1
		[Toggle(_USEAO_ON)] _Useao("Use ao?", Float) = 0
		_Ao("Ao", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 2.0
		#pragma shader_feature_local _USEAO_ON
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal2d;
		uniform sampler2D _Base2d;
		uniform sampler2D _Mask2d;
		uniform half _Aoonbasecolor;
		uniform half _Brightbess;
		uniform half _Specular;
		uniform half _Speculartint;
		uniform half _Smoothness;
		uniform half _Ao;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			o.Normal = UnpackNormal( tex2D( _Normal2d, i.uv_texcoord ) );
			half4 tex2DNode3 = tex2D( _Mask2d, i.uv_texcoord );
			half4 temp_output_26_0 = ( tex2D( _Base2d, i.uv_texcoord ) * ( 1.0 - ( ( 1.0 - tex2DNode3.r ) * _Aoonbasecolor ) ) );
			o.Albedo = ( temp_output_26_0 * _Brightbess ).rgb;
			half4 temp_cast_1 = (_Specular).xxxx;
			half4 lerpResult28 = lerp( temp_cast_1 , ( temp_output_26_0 * _Specular ) , _Speculartint);
			o.Specular = ( lerpResult28 * tex2DNode3.r ).rgb;
			o.Smoothness = ( 1.0 - ( tex2DNode3.g * _Smoothness ) );
			#ifdef _USEAO_ON
				half staticSwitch20 = ( 1.0 - ( ( 1.0 - tex2DNode3.r ) * _Ao ) );
			#else
				half staticSwitch20 = 1.0;
			#endif
			o.Occlusion = staticSwitch20;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
1920;87;1366;707;106.715;442.6939;1;True;False
Node;AmplifyShaderEditor.TexCoordVertexDataNode;27;-1843.168,261.4279;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-1555.36,422.8056;Inherit;True;Property;_Mask2d;Mask2d;2;0;Create;True;0;0;False;0;-1;None;eb7e3fa81a08523478918affac00c4bf;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;22;-823.6248,71.41966;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-877.9995,173.6614;Inherit;False;Property;_Aoonbasecolor;Ao on basecolor;7;0;Create;True;0;0;False;0;1;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-562.8446,84.31995;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1558.677,43.88509;Inherit;True;Property;_Base2d;Base2d;0;0;Create;True;0;0;False;0;-1;None;08d650021efa7c64fb47644338c9e813;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;23;-386.7066,81.67766;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;38;-1132.012,473.987;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-1197.794,541.7159;Inherit;False;Property;_Ao;Ao;9;0;Create;True;0;0;False;0;1;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-227.3701,137.7872;Inherit;False;Property;_Specular;Specular;5;0;Create;True;0;0;False;0;0.2;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-184.7637,-68.92977;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1397.349,698.0557;Inherit;False;Property;_Smoothness;Smoothness;4;0;Create;True;0;0;False;0;1;0.75;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-129.3701,304.7872;Inherit;False;Property;_Speculartint;Specular tint;6;0;Create;True;0;0;False;0;0.5;0.75;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-871.2319,486.8872;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;116.2484,209.4173;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;34;42.1947,-7.705017;Inherit;False;Property;_Brightbess;Brightbess;3;0;Create;True;0;0;False;0;1;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;28;415.8805,170.9282;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1054.349,626.0557;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;40;-695.0939,484.245;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-799.6394,363.9352;Inherit;False;Constant;_Float0;Float 0;8;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;7;-896.2087,624.2396;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;365.1947,-116.705;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;20;-444.7424,423.99;Inherit;False;Property;_Useao;Use ao?;8;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;636.2432,203.5933;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-1558.048,234.8985;Inherit;True;Property;_Normal2d;Normal2d;1;0;Create;True;0;0;False;0;-1;None;a2c5a688d96102143bb10c7196ced5e0;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1076,-107;Half;False;True;-1;0;ASEMaterialInspector;0;0;StandardSpecular;YesilayCharacter;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;1;27;0
WireConnection;22;0;3;1
WireConnection;24;0;22;0
WireConnection;24;1;25;0
WireConnection;1;1;27;0
WireConnection;23;0;24;0
WireConnection;38;0;3;1
WireConnection;26;0;1;0
WireConnection;26;1;23;0
WireConnection;39;0;38;0
WireConnection;39;1;37;0
WireConnection;32;0;26;0
WireConnection;32;1;30;0
WireConnection;28;0;30;0
WireConnection;28;1;32;0
WireConnection;28;2;31;0
WireConnection;15;0;3;2
WireConnection;15;1;16;0
WireConnection;40;0;39;0
WireConnection;7;0;15;0
WireConnection;33;0;26;0
WireConnection;33;1;34;0
WireConnection;20;1;21;0
WireConnection;20;0;40;0
WireConnection;35;0;28;0
WireConnection;35;1;3;1
WireConnection;2;1;27;0
WireConnection;0;0;33;0
WireConnection;0;1;2;0
WireConnection;0;3;35;0
WireConnection;0;4;7;0
WireConnection;0;5;20;0
ASEEND*/
//CHKSM=89829EE6559CA707A98FEF3D302D53F0ED70882F