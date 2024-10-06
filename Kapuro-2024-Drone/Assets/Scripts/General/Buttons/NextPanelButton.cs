using UnityEngine;
using UnityEngine.UI;

public class NextPanelButton : AbstractButton
{
    [SerializeField] private PanelController panelController; // パネルのコントローラー
    
    // @brief ボタン押下で次のパネルを表示
    private void NextPanel()
    {
        panelController.SetNextPanel();
    }

    // @override
    // @brief 次のパネルボタンの初期化
    public void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(NextPanel);
    }
    
    public override void Initialize()
    {
        
    }
}