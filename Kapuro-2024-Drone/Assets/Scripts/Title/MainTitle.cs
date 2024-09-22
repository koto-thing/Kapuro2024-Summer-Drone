using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainTitle : MonoBehaviour
{
    [SerializeField] private ButtonController buttonController;

    private async void Start()
    {
        var tasks = new List<UniTask>
        {
            buttonController.Initialize(), // ボタンの初期処理
        };

        await UniTask.WhenAll(tasks);
    }
    
    private void Update()
    {
        AudioController.Instance.AudioControllerUpdate();
    }
}