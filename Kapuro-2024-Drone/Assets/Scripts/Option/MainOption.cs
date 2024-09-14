using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainOption : MonoBehaviour
{
    [SerializeField] private AudioController audioController;
    [SerializeField] private ButtonController buttonController;

    // @brief 初期処理
    private async void Start()
    {
        var tasks = new List<UniTask>
        {
            buttonController.Initialize(), // ボタンの初期処理
            audioController.Initialize() // 音声の初期処理
        };
        
        await UniTask.WhenAll(tasks);
    }
}
