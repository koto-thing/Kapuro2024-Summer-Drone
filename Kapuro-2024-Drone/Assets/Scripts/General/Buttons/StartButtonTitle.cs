using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonTitle : AbstractButton
{
    [SerializeField] private MainTitle mainTitle;
    
    // @brief ボタン押下でGameシーン呼び出し
    private void StartGameButton()
    {
        mainTitle.IsStartButtonPushed = true;
    }
    
    // @override
    // @brief Startボタンの初期化
    public override void Initialize()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(StartGameButton);
    }
}
