using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainStartUp : MonoBehaviour
{
    [SerializeField] private UIManagerStartUp uiManager;

    // @brief 初期化
    private async void Start()
    {
        var tasks = new List<UniTask>
        {
            uiManager.Initialize(),
        };
        
        await UniTask.WhenAll(tasks);
    }

    // @brief 更新
    private void Update()
    {
        
    }
}