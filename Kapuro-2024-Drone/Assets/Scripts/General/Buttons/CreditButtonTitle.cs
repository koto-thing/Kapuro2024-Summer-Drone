using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditButtonTitle : AbstractButton
{
    // @brief ボタン押下でGameシーン呼び出し
    private void OptionGameButton()
    {
        SceneController.LoadScene("Credit");
    }
    
    // @override
    // @brief Startボタンの初期化
    public override void Initialize()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OptionGameButton);
    }
}