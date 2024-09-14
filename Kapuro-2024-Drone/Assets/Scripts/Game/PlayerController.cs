using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AbstractPlayers player; //　プレイヤー
    
    // @brief プレイヤーの初期化
    public async UniTask Initialize()
    {
        player.Initialize();
        
        await UniTask.CompletedTask;
    }

    // @brief プレイヤーの状態を更新
    public void PlayerUpdate()
    {
        switch(player.State)
        {
            case AbstractPlayers.PlayerState.Idle:
                player.ChangeState();
                break;
            case AbstractPlayers.PlayerState.PowerSetting:
                player.PowerSetting();
                break;
            case AbstractPlayers.PlayerState.DirectionSetting:
                player.DirectionSetting();
                break;
            case AbstractPlayers.PlayerState.Move:
                player.MoveDrone();
                break;
        }
    }
}
