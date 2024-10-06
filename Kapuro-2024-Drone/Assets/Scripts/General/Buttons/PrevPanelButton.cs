using UnityEngine;
using UnityEngine.UI;

public class PrevPanelButton : AbstractButton
{
    [SerializeField] private PanelController panelController; // パネルのコントローラー
    
    // @brief ボタン押下で前のパネルを表示
    private void PrevPanel()
    {
        panelController.SetPrevPanel();
    }

    // @override
    // @brief 前のパネルボタンの初期化
    public void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(PrevPanel);
    }
    
    public override void Initialize()
    {
        
    }
}