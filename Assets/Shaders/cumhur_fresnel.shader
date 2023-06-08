Shader "cumhur/fresnel"
{
  Properties
  {
    _TextureSample0 ("Texture Sample 0", 2D) = "white" {}
    _TextureContrast ("Texture Contrast", float) = 1
    _Float0 ("Float 0", float) = 0
    _Float1 ("Float 1", float) = 0
    _Color0 ("Color 0", Color) = (0,0,0,0)
    _Color2 ("Color 2", Color) = (0,0,0,0)
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
      uniform float4 _Color0;
      uniform float _Float0;
      uniform float _Float1;
      uniform float4 _TextureSample0_ST;
      uniform float _TextureContrast;
      uniform float4 _Color2;
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
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float3 u_xlat0_d;
      float3 u_xlat16_0;
      float3 u_xlat10_0;
      float3 u_xlat1_d;
      float4 u_xlat10_1;
      float3 u_xlat2;
      float3 u_xlat16_3;
      float3 u_xlat4;
      float3 u_xlat16_5;
      float3 u_xlat16_6;
      float3 u_xlat16_10;
      float u_xlat21;
      float u_xlat16_21;
      float u_xlat22;
      float u_xlat16_26;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xy = float2(TRANSFORM_TEX(in_f.texcoord.xy, _TextureSample0));
          u_xlat10_0.xyz = tex2D(_TextureSample0, u_xlat0_d.xy).xyz.xyz;
          u_xlat16_0.xyz = float3(log2(u_xlat10_0.xyz));
          u_xlat0_d.xyz = float3((u_xlat16_0.xyz * float3(_TextureContrast, _TextureContrast, _TextureContrast)));
          u_xlat0_d.xyz = float3(exp2(u_xlat0_d.xyz));
          u_xlat21 = length(in_f.texcoord2.xyz);
          u_xlat21 = ((u_xlat21 * _Float0) + _Float1);
          u_xlat21 = clamp(u_xlat21, 0, 1);
          u_xlat1_d.xyz = float3(((-_Color0.xyz) + float3(1, 1, 1)));
          u_xlat1_d.xyz = float3((((-u_xlat1_d.xyz) * float3(u_xlat21, u_xlat21, u_xlat21)) + float3(1, 1, 1)));
          u_xlat2.xyz = float3((float3(u_xlat21, u_xlat21, u_xlat21) * _Color2.xyz));
          u_xlat0_d.xyz = float3(((u_xlat1_d.xyz * u_xlat0_d.xyz) + u_xlat2.xyz));
          u_xlat1_d.xyz = float3(normalize(in_f.texcoord1.xyz));
          u_xlat2.xyz = float3(((-in_f.texcoord2.xyz) + _WorldSpaceCameraPos.xyz));
          u_xlat2.xyz = float3(normalize(u_xlat2.xyz));
          u_xlat21 = dot(u_xlat2.xyz, u_xlat1_d.xyz);
          u_xlat22 = (u_xlat21 + u_xlat21);
          u_xlat21 = u_xlat21;
          u_xlat21 = clamp(u_xlat21, 0, 1);
          u_xlat16_3.x = ((-u_xlat21) + 1);
          u_xlat4.xyz = float3(((u_xlat1_d.xyz * (-float3(u_xlat22, u_xlat22, u_xlat22))) + u_xlat2.xyz));
          u_xlat21 = dot(u_xlat1_d.xyz, _WorldSpaceLightPos0.xyz);
          u_xlat21 = clamp(u_xlat21, 0, 1);
          u_xlat16_10.xyz = float3((float3(u_xlat21, u_xlat21, u_xlat21) * _LightColor0.xyz));
          u_xlat21 = dot(u_xlat4.xyz, _WorldSpaceLightPos0.xyz);
          u_xlat21 = (u_xlat21 * u_xlat21);
          u_xlat1_d.x = (u_xlat21 * u_xlat21);
          u_xlat1_d.y = 1;
          u_xlat21 = tex2D(unity_NHxRoughness, u_xlat1_d.xy).x;
          u_xlat16_5.x = (u_xlat21 * 3.53466082);
          u_xlat16_5.xyz = float3(((u_xlat0_d.xyz * float3(0.779083729, 0.779083729, 0.779083729)) + u_xlat16_5.xxx));
          u_xlat16_26 = dot((-u_xlat2.xyz), in_f.texcoord1.xyz);
          u_xlat16_26 = (u_xlat16_26 + u_xlat16_26);
          u_xlat16_6.xyz = float3(((in_f.texcoord1.xyz * (-float3(u_xlat16_26, u_xlat16_26, u_xlat16_26))) + (-u_xlat2.xyz)));
          u_xlat10_1 = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, float4(u_xlat16_6.xyz, 6));
          u_xlat16_26 = (u_xlat10_1.w + (-1));
          u_xlat16_26 = ((unity_SpecCube0_HDR.w * u_xlat16_26) + 1);
          u_xlat16_26 = (u_xlat16_26 * unity_SpecCube0_HDR.x);
          u_xlat16_6.xyz = float3((u_xlat10_1.xyz * float3(u_xlat16_26, u_xlat16_26, u_xlat16_26)));
          u_xlat16_21 = (u_xlat16_3.x * u_xlat16_3.x);
          u_xlat16_21 = (u_xlat16_3.x * u_xlat16_21);
          u_xlat16_21 = (u_xlat16_3.x * u_xlat16_21);
          u_xlat16_3.x = ((u_xlat16_21 * (-2.98023224E-08)) + 0.220916301);
          u_xlat16_6.xyz = float3((u_xlat16_3.xxx * u_xlat16_6.xyz));
          u_xlat16_3.xyz = float3(((u_xlat16_5.xyz * u_xlat16_10.xyz) + u_xlat16_6.xyz));
          out_f.color.xyz = float3((u_xlat0_d.xyz + u_xlat16_3.xyz));
          out_f.color.w = 1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: ShadowCaster
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
      #pragma multi_compile SHADOWS_DEPTH
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
      };
      
      struct OUT_Data_Vert
      {
          float3 texcoord1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 texcoord1 :TEXCOORD1;
          float4 vertex :Position;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float4 u_xlat2;
      float u_xlat6;
      float u_xlat9;
      int u_xlatb9;
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
          u_xlat0.xyz = float3((((-u_xlat0.xyz) * float3(u_xlat9, u_xlat9, u_xlat9)) + u_xlat1.xyz));
          if((unity_LightShadowBias.z!=0))
          {
              u_xlatb9 = 1;
          }
          else
          {
              u_xlatb9 = 0;
          }
          float _tmp_dvx_7 = int(u_xlatb9);
          u_xlat0.xyz = float3(_tmp_dvx_7, _tmp_dvx_7, _tmp_dvx_7);
          u_xlat0 = mul(unity_MatrixVP, u_xlat0);
          u_xlat1.x = (unity_LightShadowBias.x / u_xlat0.w);
          u_xlat1.x = clamp(u_xlat1.x, 0, 1);
          u_xlat6 = (u_xlat0.z + u_xlat1.x);
          u_xlat1.x = max((-u_xlat0.w), u_xlat6);
          out_v.vertex.xyw = u_xlat0.xyw;
          u_xlat0.x = ((-u_xlat6) + u_xlat1.x);
          out_v.vertex.z = ((unity_LightShadowBias.y * u_xlat0.x) + u_xlat6);
          u_xlat0.xyz = float3((in_v.vertex.yyy * conv_mxt4x4_1(unity_ObjectToWorld).xyz));
          u_xlat0.xyz = float3(((conv_mxt4x4_0(unity_ObjectToWorld).xyz * in_v.vertex.xxx) + u_xlat0.xyz));
          u_xlat0.xyz = float3(((conv_mxt4x4_2(unity_ObjectToWorld).xyz * in_v.vertex.zzz) + u_xlat0.xyz));
          out_v.texcoord1.xyz = float3(((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz));
          //return u_xlat0.xyz;
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
