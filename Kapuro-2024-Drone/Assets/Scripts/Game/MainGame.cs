using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MainGame : MonoBehaviour
{
    public enum GameState { START, INGAME, FINISH }
    [SerializeField] private AssetLoader assetLoader;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ButtonController buttonController;
    [SerializeField] private TargetController targetController;

    [SerializeField] private GameState gameState;
    
    private async void Start()
    {
        // ゲームを非同期で初期化
        var tasks = new List<UniTask> 
        {
            assetLoader.LoadAssetsAsync(),
            playerController.Initialize(),
            buttonController.Initialize(),
            targetController.Initialize()
        };
        
        await UniTask.WhenAll(tasks); // 全ての初期化が終わるまで待機
    }

    private void Update()
    {
        switch (gameState)
        {
            /* ゲーム開始前の処理 */
            case GameState.START:
                ChangeState(GameState.INGAME);
                break;
            
            /* ゲーム中の処理 */
            case GameState.INGAME:
                // 更新処理関係
                playerController.PlayerUpdate();
                targetController.TargetControllerUpdate();
                
                // ゲーム終了判定
                if (targetController.FinishGameHandler())
                    ChangeState(GameState.FINISH);
                break;
            
            /* ゲーム終了時の処理 */
            case GameState.FINISH:
                SceneController.LoadScene("Title");
                break;
        }
    }

    private void ChangeState(GameState nextState)
    {
        gameState = nextState;
    }
}
