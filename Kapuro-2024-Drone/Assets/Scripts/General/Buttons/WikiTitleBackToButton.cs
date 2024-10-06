using UnityEngine;
using UnityEngine.UI;

public class WikiTitleBackToButton : AbstractButton
{
    [SerializeField] private MainTitle mainTitle;
    
    // @brief ボタン押下でTitleシーン呼び出し
    private void BackToTitleButton()
    {
        mainTitle.IsWikiBackToTitleButtonPushed = true;    
    }
    
    // @override
    // @brief Titleボタンの初期化
    public override void Initialize()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(BackToTitleButton);
    }
}