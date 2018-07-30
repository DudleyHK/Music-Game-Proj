// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

   Shader "Vertex Modifier" {
     Properties {
       _MainTex ("Texture", 2D) = "white" {}
       _Amount ("Amount", Float) = 1.0
     }
     SubShader {
       Tags { "RenderType" = "Opaque" }
       CGPROGRAM
       #pragma surface surf Lambert vertex:vert
       struct Input {
           float2 uv_MainTex;
       };
 
       // Access the shaderlab properties
       float _Amount;
       sampler2D _MainTex;
 
       // Vertex modifier function
       void vert (inout appdata_full v) 
	   {
              float3 wpos = round(mul(unity_ObjectToWorld, v.vertex) );
              

       }
 
       // Surface shader function
       void surf (Input IN, inout SurfaceOutput o) {
           o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
       }
       ENDCG
     }
   }