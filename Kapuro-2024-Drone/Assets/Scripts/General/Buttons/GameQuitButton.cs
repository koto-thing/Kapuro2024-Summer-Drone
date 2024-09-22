using UnityEngine;
using UnityEngine.UI;

public class GameQuitButton : AbstractButton
{
    // @brief ボタン押下でGameシーン呼び出し
    private void OptionGameButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Editorの再生終了
        #else
            Application.Quit(); // Applicationの終了
        #endif
    }
    
    // @override
    // @brief Startボタンの初期化
    public override void Initialize()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OptionGameButton);
    }
}