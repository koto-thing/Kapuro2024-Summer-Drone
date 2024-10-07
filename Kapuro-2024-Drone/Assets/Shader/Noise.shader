Shader "Custom/RandomNoise"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Speed ("Noise Speed", Range(0.0, 10.0)) = 1.0 // ノイズ変化の速度
        _Alpha ("Alpha", Range(0.0, 1.0)) = 1.0 // アルファ値のプロパティ（デフォルト値）
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" } // 透明描画用のタグ設定
        Blend SrcAlpha OneMinusSrcAlpha      // アルファブレンディングを設定
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        float _Speed;  // ノイズ変化の速度
        float _Alpha;  // アルファ値のプロパティ

        struct Input
        {
            float2 uv_MainTex;
            float4 color; // SpriteRenderer の color プロパティを受け取る
        };

        // ノイズ生成のためのランダム関数
        float random(fixed2 p)
        {
            return frac(sin(dot(p, fixed2(12.9898, 78.233))) * 43758.5453);
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.uv_MainTex;
            
            // ノイズパターンの変化を時間と共に計算
            float noise = random(uv + _Time.y * _Speed);

            // ノイズパターンをアルベド色（RGB）に適用
            o.Albedo = fixed4(noise, noise, noise, 1.0);

            // `SpriteRenderer` のアルファ値と `_Alpha` プロパティを掛け合わせた値を最終アルファ値に設定
            o.Alpha = _Alpha * IN.color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
