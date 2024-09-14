using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private List<Target> targets; // ターゲットのリスト
    
    // @brief 初期化
    public async UniTask Initialize()
    {
        targets = new List<Target>(FindObjectsByType<Target>( FindObjectsSortMode.None ));
        
        await UniTask.CompletedTask;
    }

    public void TargetControllerUpdate()
    {
        targets.RemoveAll(target => target.IsCollided); // 衝突したターゲットを削除
    }
    
    // @brief 
    public bool FinishGameHandler()
    {
        if (targets.Count == 0)
            return true;
        
        return false;
    }
}