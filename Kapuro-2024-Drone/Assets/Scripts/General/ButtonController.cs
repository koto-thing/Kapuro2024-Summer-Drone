using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private List<AbstractButton> buttons;
    
    // @brief ボタンの初期化
    public void Initialize()
    {
        foreach(var button in buttons)
        {
            button.Initialize();
        }
    }
}