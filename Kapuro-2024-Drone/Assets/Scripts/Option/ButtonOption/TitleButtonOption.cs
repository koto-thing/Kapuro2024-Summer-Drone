using UnityEngine.UI;

public class TitleButtonOption : AbstractButton
{
    // @brief ボタン押下でGameシーン呼び出し
    private void StartGameButton()
    {
        SceneController.LoadScene("Title");
    }
    
    // @override
    // @brief Startボタンの初期化
    public override void Initialize()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(StartGameButton);
    }
}
