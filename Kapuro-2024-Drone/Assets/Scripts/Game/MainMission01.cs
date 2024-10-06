using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FMODUnity;
using UnityEngine;

public class MainMission01 : AbstractGameMain
{
    private enum GameState { START, INGAME, FINISH }
    
    [Header("Game Controllers")]
    [SerializeField] private AssetLoader assetLoader;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ButtonController buttonController;
    [SerializeField] private TargetController targetController;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private ObstacleFactryController obstacleFactryController;

    [Header("Game State")]
    [SerializeField] private GameState gameState;
    
    [Header("Flag")]
    [SerializeField] private bool isLastDialogueLoaded = false;
    
    public override async void Initialize()
    {
        // ゲームを非同期で初期化
        var tasks = new List<UniTask> 
        {
            assetLoader.LoadAssetsAsync(),
            playerController.Initialize(),
            buttonController.Initialize(),
            targetController.Initialize(),
            dialogueController.Initialize(),
            obstacleFactryController.Initialize()
        };
        
        await UniTask.WhenAll(tasks); // 全ての初期化が終わるまで待機
    }

    public override void MainUpdate()
    {
        switch (gameState)
        {
            /* ゲーム開始前の処理 */
            case GameState.START:
                if(buttonController.IsPopUpButtonPushed)
                    ChangeState(GameState.INGAME);
                break;
            
            /* ゲーム中の処理 */
            case GameState.INGAME:
                // 更新処理関係
                playerController.PlayerUpdate();
                obstacleFactryController.ObstacleFactoryUpdate();
                targetController.TargetControllerUpdate();
                
                // ゲーム終了判定
                if (targetController.FinishGameHandler())
                {
                    GetComponent<StudioEventEmitter>().Play();
                    ChangeState(GameState.FINISH);
                }
                break;
            
            /* ゲーム終了時の処理 */
            case GameState.FINISH:
                if (isLastDialogueLoaded != true)
                {
                    dialogueController.LoadDialogue();
                    isLastDialogueLoaded = true;
                }
                
                if(dialogueController.IsFirstDialogueFinished)
                    SceneController.LoadScene("Title");
                break;
        }
        
        AudioController.Instance.AudioControllerUpdate();
        dialogueController.DialogueControllerUpdate();
    }

    private void ChangeState(GameState nextState)
    {
        gameState = nextState;
    }
}