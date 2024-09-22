using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpButton : AbstractButton
{
    [SerializeField] private event Action onPopUpButtonPushed;
    public event Action OnPopUpButtonPushed
    {
        add => onPopUpButtonPushed += value;
        remove => onPopUpButtonPushed -= value; 
    }
    
    // @brief ボタン押下でGameシーン呼び出し
    private void DestroyPopUp()
    {
        onPopUpButtonPushed?.Invoke();
        Destroy(transform.parent.gameObject);
    }
    
    // @override
    // @brief Startボタンの初期化
    public override void Initialize()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(DestroyPopUp);
    }
}