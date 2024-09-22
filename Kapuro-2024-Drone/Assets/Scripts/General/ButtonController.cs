using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private List<AbstractButton> buttons;
    
    [SerializeField] private bool isPopUpButtonPushed;
    public bool IsPopUpButtonPushed { get { return isPopUpButtonPushed; } }
    
    // @brief ボタンの初期化
    public async UniTask Initialize()
    {
        buttons = new List<AbstractButton>( FindObjectsByType<AbstractButton>(FindObjectsSortMode.None) );
        foreach(var button in buttons)
        {
            button.Initialize();

            if (button is PopUpButton popUpButton)
                popUpButton.OnPopUpButtonPushed += HandlePopUpButtonPushed;
        }
        
        await UniTask.CompletedTask;
    }
    
    private void HandlePopUpButtonPushed()
    {
        isPopUpButtonPushed = true;
    }
}