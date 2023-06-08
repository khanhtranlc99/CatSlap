Shader "fresnel"
{
  Properties
  {
    _Fresnel ("Fresnel", Color) = (1,0.990384,0.1933962,0)
    _FresnelPower ("Fresnel Power", float) = 0
    _FresnelScale ("Fresnel Scale", float) = 0
    _FresnelBias ("Fresnel Bias", float) = 0
    _TextureSample0 ("Texture Sample 0", 2D) = "white" {}
    _TextureContrast ("Texture Contrast", float) = 1
    [HideInInspector] _texcoord ("", 2D) = "white" {}
    [HideInInspector] __dirty ("", float) = 1
  }
  SubShader
  {
    Tags
    { 
      "IsEmissive" = "true"
      "QUEUE" = "Background+0"
      "RenderType" = "Opaque"
    }
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "IsEmissive" = "true"
        "LIGHTMODE" = "FORWARDBASE"
        "QUEUE" = "Background+0"
        "RenderType" = "Opaque"
        "SHADOWSUPPORT" = "true"
      }
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile DIRECTIONAL
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _texcoord_ST;
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _WorldSpaceLightPos0;
      //uniform float4 unity_SpecCube0_HDR;
      uniform float4 _LightColor0;
      uniform float4 _TextureSample0_ST;
      uniform float _TextureContrast;
      uniform float4 _Fresnel;
      uniform float _FresnelBias;
      uniform float _FresnelScale;
      uniform float _FresnelPower;
      uniform sampler2D _TextureSample0;
      uniform sampler2D unity_NHxRoughness;
      //uniform samplerCUBE unity_SpecCube0;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
          float4 texcoord2 :TEXCOORD2;
          float4 texcoord5 :TEXCOORD5;
          float4 texcoord6 :TEXCOORD6;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
          float4 texcoord2 :TEXCOORD2;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float u_xlat6;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0 = (in_v.vertex.yyyy * conv_mxt4x4_1(unity_ObjectToWorld));
          u_xlat0 = ((conv_mxt4x4_0(unity_ObjectToWorld) * in_v.vertex.xxxx) + u_xlat0);
          u_xlat0 = ((conv_mxt4x4_2(unity_ObjectToWorld) * in_v.vertex.zzzz) + u_xlat0);
          u_xlat1 = (u_xlat0 + conv_mxt4x4_3(unity_ObjectToWorld));
          out_v.texcoord2.xyz = float3(((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz));
          out_v.vertex = mul(unity_MatrixVP, u_xlat1);
          out_v.texcoord.xy = float2(TRANSFORM_TEX(in_v.texcoord.xy, _texcoord));
          u_xlat0.x = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat0.y = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat0.z = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          out_v.texcoord1.xyz = float3(normalize(u_xlat0.xyz));
          out_v.texcoord2.w = 0;
          out_v.texcoord5 = float4(0, 0, 0, 0);
          out_v.texcoord6 = float4(0, 0, 0, 0);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float3 u_xlat0_d;
      float3 u_xlat16_1;
      float4 u_xlat10_1;
      float3 u_xlat16_2;
      float3 u_xlat3;
      float3 u_xlat4;
      float3 u_xlat16_5;
      float3 u_xlat16_6;
      float3 u_xlat7;
      float3 u_xlat16_7;
      float3 u_xlat10_7;
      float u_xlat21;
      float u_xlat16_23;
      float u_xlat24;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xyz = float3(((-in_f.texcoord2.xyz) + _WorldSpaceCameraPos.xyz));
          u_xlat0_d.xyz = float3(normalize(u_xlat0_d.xyz));
          u_xlat16_1.x = dot((-u_xlat0_d.xyz), in_f.texcoord1.xyz);
          u_xlat16_1.x = (u_xlat16_1.x + u_xlat16_1.x);
          u_xlat16_1.xyz = float3(((in_f.texcoord1.xyz * (-u_xlat16_1.xxx)) + (-u_xlat0_d.xyz)));
          u_xlat10_1 = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, float4(u_xlat16_1.xyz, 6));
          u_xlat16_2.x = (u_xlat10_1.w + (-1));
          u_xlat16_2.x = ((unity_SpecCube0_HDR.w * u_xlat16_2.x) + 1);
          u_xlat16_2.x = (u_xlat16_2.x * unity_SpecCube0_HDR.x);
          u_xlat16_2.xyz = float3((u_xlat10_1.xyz * u_xlat16_2.xxx));
          u_xlat3.xyz = float3(normalize(in_f.texcoord1.xyz));
          u_xlat21 = dot(u_xlat0_d.xyz, u_xlat3.xyz);
          u_xlat24 = u_xlat21;
          u_xlat24 = clamp(u_xlat24, 0, 1);
          u_xlat21 = (u_xlat21 + u_xlat21);
          u_xlat4.xyz = float3(((u_xlat3.xyz * (-float3(u_xlat21, u_xlat21, u_xlat21))) + u_xlat0_d.xyz));
          u_xlat0_d.x = dot(in_f.texcoord1.xyz, u_xlat0_d.xyz);
          u_xlat0_d.x = ((-u_xlat0_d.x) + 1);
          u_xlat0_d.x = log2(u_xlat0_d.x);
          u_xlat0_d.x = (u_xlat0_d.x * _FresnelPower);
          u_xlat0_d.x = exp2(u_xlat0_d.x);
          u_xlat0_d.x = ((_FresnelScale * u_xlat0_d.x) + _FresnelBias);
          u_xlat7.x = dot(u_xlat3.xyz, _WorldSpaceLightPos0.xyz);
          u_xlat7.x = clamp(u_xlat7.x, 0, 1);
          u_xlat16_5.xyz = float3((u_xlat7.xxx * _LightColor0.xyz));
          u_xlat7.x = dot(u_xlat4.xyz, _WorldSpaceLightPos0.xyz);
          u_xlat7.x = (u_xlat7.x * u_xlat7.x);
          u_xlat3.x = (u_xlat7.x * u_xlat7.x);
          u_xlat16_23 = ((-u_xlat24) + 1);
          u_xlat16_7.x = (u_xlat16_23 * u_xlat16_23);
          u_xlat16_7.x = (u_xlat16_23 * u_xlat16_7.x);
          u_xlat16_7.x = (u_xlat16_23 * u_xlat16_7.x);
          u_xlat16_23 = ((u_xlat16_7.x * (-2.98023224E-08)) + 0.220916301);
          u_xlat16_2.xyz = float3((float3(u_xlat16_23, u_xlat16_23, u_xlat16_23) * u_xlat16_2.xyz));
          u_xlat3.y = 1;
          u_xlat7.x = tex2D(unity_NHxRoughness, u_xlat3.xy).x;
          u_xlat16_23 = (u_xlat7.x * 3.53466082);
          u_xlat7.xy = float2(TRANSFORM_TEX(in_f.texcoord.xy, _TextureSample0));
          u_xlat10_7.xyz = tex2D(_TextureSample0, u_xlat7.xy).xyz.xyz;
          u_xlat16_7.xyz = float3(log2(u_xlat10_7.xyz));
          u_xlat7.xyz = float3((u_xlat16_7.xyz * float3(_TextureContrast, _TextureContrast, _TextureContrast)));
          u_xlat7.xyz = float3(exp2(u_xlat7.xyz));
          u_xlat16_6.xyz = float3(((u_xlat7.xyz * float3(0.779083729, 0.779083729, 0.779083729)) + float3(u_xlat16_23, u_xlat16_23, u_xlat16_23)));
          u_xlat0_d.xyz = float3(((_Fresnel.xyz * u_xlat0_d.xxx) + u_xlat7.xyz));
          u_xlat16_2.xyz = float3(((u_xlat16_6.xyz * u_xlat16_5.xyz) + u_xlat16_2.xyz));
          out_f.color.xyz = float3((u_xlat0_d.xyz + u_xlat16_2.xyz));
          out_f.color.w = 1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "IsEmissive" = "true"
        "LIGHTMODE" = "FORWARDADD"
        "QUEUE" = "Background+0"
        "RenderType" = "Opaque"
        "SHADOWSUPPORT" = "true"
      }
      ZWrite Off
      Blend One One
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile POINT
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4x4 unity_WorldToLight;
      uniform float4 _texcoord_ST;
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform float4 _TextureSample0_ST;
      uniform float _TextureContrast;
      uniform sampler2D _TextureSample0;
      uniform sampler2D _LightTexture0;
      uniform sampler2D unity_NHxRoughness;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
          float3 texcoord2 :TEXCOORD2;
          float3 texcoord3 :TEXCOORD3;
          float4 texcoord4 :TEXCOORD4;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
          float3 texcoord2 :TEXCOORD2;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float4 u_xlat2;
      float u_xlat10;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat1 = mul(unity_ObjectToWorld, float4(in_v.vertex.xyz,1.0));
          out_v.vertex = mul(unity_MatrixVP, u_xlat1);
          out_v.texcoord.xy = float2(TRANSFORM_TEX(in_v.texcoord.xy, _texcoord));
          u_xlat1.x = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat1.y = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat1.z = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          out_v.texcoord1.xyz = float3(normalize(u_xlat1.xyz));
          out_v.texcoord2.xyz = float3(((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz));
          u_xlat0 = ((conv_mxt4x4_3(unity_ObjectToWorld) * in_v.vertex.wwww) + u_xlat0);
          u_xlat1.xyz = float3((u_xlat0.yyy * conv_mxt4x4_1(unity_WorldToLight).xyz));
          u_xlat1.xyz = float3(((conv_mxt4x4_0(unity_WorldToLight).xyz * u_xlat0.xxx) + u_xlat1.xyz));
          u_xlat0.xyz = float3(((conv_mxt4x4_2(unity_WorldToLight).xyz * u_xlat0.zzz) + u_xlat1.xyz));
          out_v.texcoord3.xyz = float3(((conv_mxt4x4_3(unity_WorldToLight).xyz * u_xlat0.www) + u_xlat0.xyz));
          out_v.texcoord4 = float4(0, 0, 0, 0);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float4 u_xlat16_0;
      float4 u_xlat10_0;
      float3 u_xlat1_d;
      float3 u_xlat2_d;
      float3 u_xlat16_3;
      float3 u_xlat16_4;
      float u_xlat5;
      float u_xlat15;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xyz = float3(((-in_f.texcoord2.xyz) + _WorldSpaceCameraPos.xyz));
          u_xlat0_d.xyz = float3(normalize(u_xlat0_d.xyz));
          u_xlat1_d.xyz = float3(normalize(in_f.texcoord1.xyz));
          u_xlat15 = dot(u_xlat0_d.xyz, u_xlat1_d.xyz);
          u_xlat15 = (u_xlat15 + u_xlat15);
          u_xlat0_d.xyz = float3(((u_xlat1_d.xyz * (-float3(u_xlat15, u_xlat15, u_xlat15))) + u_xlat0_d.xyz));
          u_xlat2_d.xyz = float3(((-in_f.texcoord2.xyz) + _WorldSpaceLightPos0.xyz));
          u_xlat2_d.xyz = float3(normalize(u_xlat2_d.xyz));
          u_xlat0_d.x = dot(u_xlat0_d.xyz, u_xlat2_d.xyz);
          u_xlat5 = dot(u_xlat1_d.xyz, u_xlat2_d.xyz);
          u_xlat5 = clamp(u_xlat5, 0, 1);
          u_xlat0_d.x = (u_xlat0_d.x * u_xlat0_d.x);
          u_xlat1_d.x = (u_xlat0_d.x * u_xlat0_d.x);
          u_xlat1_d.y = 1;
          u_xlat0_d.x = tex2D(unity_NHxRoughness, u_xlat1_d.xy).x;
          u_xlat16_3.x = (u_xlat0_d.x * 3.53466082);
          u_xlat0_d.xz = TRANSFORM_TEX(in_f.texcoord.xy, _TextureSample0);
          u_xlat10_0.xzw = tex2D(_TextureSample0, u_xlat0_d.xz).xyz.xyz;
          u_xlat16_0.xzw = log2(u_xlat10_0.xzw);
          u_xlat0_d.xzw = (u_xlat16_0.xzw * float3(_TextureContrast, _TextureContrast, _TextureContrast));
          u_xlat0_d.xzw = exp2(u_xlat0_d.xzw);
          u_xlat16_3.xyz = float3(((u_xlat0_d.xzw * float3(0.779083729, 0.779083729, 0.779083729)) + u_xlat16_3.xxx));
          u_xlat0_d.xzw = (in_f.texcoord2.yyy * conv_mxt4x4_1(unity_WorldToLight).xyz);
          u_xlat0_d.xzw = ((conv_mxt4x4_0(unity_WorldToLight).xyz * in_f.texcoord2.xxx) + u_xlat0_d.xzw);
          u_xlat0_d.xzw = ((conv_mxt4x4_2(unity_WorldToLight).xyz * in_f.texcoord2.zzz) + u_xlat0_d.xzw);
          u_xlat0_d.xzw = (u_xlat0_d.xzw + conv_mxt4x4_3(unity_WorldToLight).xyz);
          u_xlat0_d.x = dot(u_xlat0_d.xzw, u_xlat0_d.xzw);
          u_xlat0_d.x = tex2D(_LightTexture0, u_xlat0_d.xx).x;
          u_xlat16_4.xyz = float3((u_xlat0_d.xxx * _LightColor0.xyz));
          u_xlat16_4.xyz = float3((float3(u_xlat5, u_xlat5, u_xlat5) * u_xlat16_4.xyz));
          out_f.color.xyz = float3((u_xlat16_3.xyz * u_xlat16_4.xyz));
          out_f.color.w = 1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 3, name: ShadowCaster
    {
      Name "ShadowCaster"
      Tags
      { 
        "IsEmissive" = "true"
        "LIGHTMODE" = "SHADOWCASTER"
        "QUEUE" = "Background+0"
        "RenderType" = "Opaque"
        "SHADOWSUPPORT" = "true"
      }
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile SHADOWS_DEPTH UNITY_PASS_SHADOWCASTER
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 _WorldSpaceLightPos0;
      //uniform float4 unity_LightShadowBias;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord1 :TEXCOORD1;
          float3 texcoord2 :TEXCOORD2;
          float3 texcoord3 :TEXCOORD3;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord1 :TEXCOORD1;
          float3 texcoord2 :TEXCOORD2;
          float3 texcoord3 :TEXCOORD3;
          float4 vertex :Position;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      int u_xlatb0;
      float4 u_xlat1;
      float4 u_xlat2;
      float u_xlat6;
      float u_xlat9;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0.x = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat0.y = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat0.z = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          u_xlat0.xyz = float3(normalize(u_xlat0.xyz));
          u_xlat1 = mul(unity_ObjectToWorld, in_v.vertex);
          u_xlat2.xyz = float3((((-u_xlat1.xyz) * _WorldSpaceLightPos0.www) + _WorldSpaceLightPos0.xyz));
          u_xlat2.xyz = float3(normalize(u_xlat2.xyz));
          u_xlat9 = dot(u_xlat0.xyz, u_xlat2.xyz);
          u_xlat9 = (((-u_xlat9) * u_xlat9) + 1);
          u_xlat9 = sqrt(u_xlat9);
          u_xlat9 = (u_xlat9 * unity_LightShadowBias.z);
          u_xlat2.xyz = float3((((-u_xlat0.xyz) * float3(u_xlat9, u_xlat9, u_xlat9)) + u_xlat1.xyz));
          out_v.texcoord3.xyz = float3(u_xlat0.xyz);
          if((unity_LightShadowBias.z!=0))
          {
              u_xlatb0 = 1;
          }
          else
          {
              u_xlatb0 = 0;
          }
          float _tmp_dvx_6 = int(u_xlatb0);
          u_xlat0.xyz = float3(_tmp_dvx_6, _tmp_dvx_6, _tmp_dvx_6);
          u_xlat0 = mul(unity_MatrixVP, u_xlat0);
          u_xlat1.x = (unity_LightShadowBias.x / u_xlat0.w);
          u_xlat1.x = clamp(u_xlat1.x, 0, 1);
          u_xlat6 = (u_xlat0.z + u_xlat1.x);
          u_xlat1.x = max((-u_xlat0.w), u_xlat6);
          out_v.vertex.xyw = u_xlat0.xyw;
          u_xlat0.x = ((-u_xlat6) + u_xlat1.x);
          out_v.vertex.z = ((unity_LightShadowBias.y * u_xlat0.x) + u_xlat6);
          out_v.texcoord1.xy = float2(in_v.texcoord.xy);
          u_xlat0.xyz = float3((in_v.vertex.yyy * conv_mxt4x4_1(unity_ObjectToWorld).xyz));
          u_xlat0.xyz = float3(((conv_mxt4x4_0(unity_ObjectToWorld).xyz * in_v.vertex.xxx) + u_xlat0.xyz));
          u_xlat0.xyz = float3(((conv_mxt4x4_2(unity_ObjectToWorld).xyz * in_v.vertex.zzz) + u_xlat0.xyz));
          out_v.texcoord2.xyz = float3(((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz));
          //return u_xlat2.xyz;
          //return u_xlat1.xyz;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          out_f.color = float4(0, 0, 0, 0);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "Diffuse"
}
