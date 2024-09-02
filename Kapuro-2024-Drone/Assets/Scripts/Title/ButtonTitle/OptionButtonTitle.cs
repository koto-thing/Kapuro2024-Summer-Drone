using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionButtonTitle : AbstractButton
{
    // @brief ボタン押下でGameシーン呼び出し
    private void OptionGameButton()
    {
        SceneController.LoadScene("Option");
    }
    
    // @override
    // @brief Startボタンの初期化
    public override void Initialize()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OptionGameButton);
    }
}
