using UnityEngine;
using UnityEngine.UI;

public class Title_TutorialSelectButton : AbstractButton
{
    // @brief ボタン押下でTutorialシーン呼び出し
    private void TutorialSelectButton()
    {
        GameInfo.currentGameType = GameInfo.GameType.Tutorial;
        SceneController.LoadScene("Game");
    }
    
    // @override
    // @brief Tutorialボタンの初期化
    public override void Initialize()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TutorialSelectButton);
    }
}