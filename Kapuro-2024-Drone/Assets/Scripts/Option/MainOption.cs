using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainOption : MonoBehaviour
{
    [SerializeField] private ButtonController buttonController;

    // @brief 初期処理
    private async void Start()
    {
        var tasks = new List<UniTask>
        {
            buttonController.Initialize(), // ボタンの初期処理
            AudioController.Instance.SetSlider() // 音量の初期処理
        };
        
        await UniTask.WhenAll(tasks);
    }
    
    // @brief 更新処理
    private void Update()
    {
        AudioController.Instance.AudioControllerUpdate(); // 更新処理
    }
}
