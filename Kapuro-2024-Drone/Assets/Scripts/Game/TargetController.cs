using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private List<Target> targets; // ターゲットのリスト
    
    [Header("残りの目標")]
    [SerializeField] private TextMeshProUGUI targetCountText; // ターゲットのカウントテキスト
    
    // @brief 初期化
    public async UniTask Initialize()
    {
        targets = new List<Target>(FindObjectsByType<Target>( FindObjectsSortMode.None ));
        targetCountText.text = targets.Count.ToString();
        
        await UniTask.CompletedTask;
    }

    public void TargetControllerUpdate()
    {
        targets.RemoveAll(target => target.IsCollided); // 衝突したターゲットを削除
        targetCountText.text = targets.Count.ToString(); // ターゲットのカウントを更新
    }
    
    // @brief 
    public bool FinishGameHandler()
    {
        if (targets.Count == 0)
            return true;
        
        return false;
    }
}