using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private List<AbstractButton> buttons;
    
    // @brief ボタンの初期化
    public async UniTask Initialize()
    {
        foreach(var button in buttons)
        {
            button.Initialize();
        }
        
        await UniTask.CompletedTask;
    }
}