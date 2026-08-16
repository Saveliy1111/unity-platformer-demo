Shader "Custom/LowHealthVignetteCode"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Vignette Color", Color) = (0.8, 0, 0, 1)
        _VignettePower ("Vignette Power", Range(0.1, 5.0)) = 2.5
        _PulseSpeed ("Pulse Speed", Range(0.1, 15.0)) = 6.0
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            cbuffer UnityPerMaterial
            {
                float4 _Color;
                float _VignettePower;
                float _PulseSpeed;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dist = distance(i.uv, float2(0.5, 0.5));
                
                float vignette = pow(dist, _VignettePower);
                
                float pulse = (sin(_Time.y * _PulseSpeed) * 0.5 + 0.5) * 0.7 + 0.3;
                
                float finalAlpha = vignette * pulse;
                
                return float4(_Color.rgb, finalAlpha * _Color.a);
            }
            ENDCG
        }
    }
}