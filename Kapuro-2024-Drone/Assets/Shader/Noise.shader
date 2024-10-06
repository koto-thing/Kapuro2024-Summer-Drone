Shader "Custom/RandomNoise"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Speed ("Noise Speed", Range(0.0, 10.0)) = 1.0 // ノイズ変化の速度
        _Alpha ("Alpha", Range(0.0, 1.0)) = 1.0 // アルファ値のプロパティ
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" } // タグを透明に変更
        Blend SrcAlpha OneMinusSrcAlpha // アルファブレンディングを有効にする
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        float _Speed;  // ノイズの変化速度
        float _Alpha;  // アルファ値

        struct Input
        {
            float2 uv_MainTex;
        };

        // 乱数生成用の関数
        float random(fixed2 p)
        {
            return frac(sin(dot(p, fixed2(12.9898, 78.233))) * 43758.5453);
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // ノイズの座標に時間による変化を加える
            float2 uv = IN.uv_MainTex;

            // `uv`に時間に応じたオフセットを加えて、ノイズパターンを変化させる
            float c = random(uv + _Time.y * _Speed);

            // グレースケールのノイズを生成
            o.Albedo = fixed4(c, c, c, _Alpha); // アルファ値を使用して透明度を設定
        }
        ENDCG
    }
    FallBack "Diffuse"
}
