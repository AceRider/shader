﻿Shader "Holistic/SphericalFog"
{
    Properties
    {
        _FogCentre("Fog Centre/Radius", Vector) = (0,0,0,0.5)
        _FogColor("Fog Colour", Color) = (1,1,1,1)
        _InnerRatio("Inner Ratio", Range (0.0, 0.9)) = 0.5
        _Density("Density", Range (0.0, 1.0)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off Lighting Off ZWrite Off
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float3 view : TEXCOORD0;
                float4 pos : SV_POSITION;
                float4 projPos : TEXCOORD1;
            };

            float4 _FogCentre;
            fixed4 _FogColor;
            float _InnerRatio;
            float _Density;
            sampler2D _CameraDepthTexture;

            v2f vert (appdata_base v)
            {
                v2f o;
                float4 wPos = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.view = wPos.xyz - _WorldSpaceCameraPos;
                o.projPos = ComputeScreenPos(o.pos);

                float inFrontOf = (o.pos.z / o.pos.w) > 0;
                o.pos.z *= inFrontOf;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                half4 col = half4(1,1,1,1);
                return col;
            }
            ENDCG
        }
    }
}
