using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MainTitle : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private ButtonController buttonController;

    [Header("Canvas")]
    [SerializeField] private Canvas titleCanvas; 
    [SerializeField] private Canvas startCanvas;
    [SerializeField] private Canvas wikiCanvas;
    
    [Header("Animation BackGround")]
    [SerializeField] private GameObject backgroundAnimation;
    
    [Header("Flag")] 
    [SerializeField] private bool isStartButtonPushed = false;
    [SerializeField] private bool isBackToTitleButtonPushed = false;
    [SerializeField] private bool isWikiBackToTitleButtonPushed = false;
    [SerializeField] private bool isWikiButtonPushed = false;
    
    public bool IsStartButtonPushed { get { return isStartButtonPushed; } set { isStartButtonPushed = value; } }
    public bool IsBackToTitleButtonPushed { get { return isBackToTitleButtonPushed; } set { isBackToTitleButtonPushed = value; } }
    public bool IsWikiBackToTitleButtonPushed { get { return isWikiBackToTitleButtonPushed; } set { isWikiBackToTitleButtonPushed = value; } }
    public bool IsWikiButtonPushed { get { return isWikiButtonPushed; } set { isWikiButtonPushed = value; } }
    
    private async void Start()
    {
        var tasks = new List<UniTask>
        {
            buttonController.Initialize(), // ボタンの初期処理
            
        };

        startCanvas.gameObject.SetActive(false);
        wikiCanvas.gameObject.SetActive(false);
        
        await UniTask.WhenAll(tasks);
    }
    
    private void Update()
    {
        AudioController.Instance.AudioControllerUpdate();

        if (isStartButtonPushed)
        {
            ChangeCanvas(titleCanvas, startCanvas);
            isStartButtonPushed = false;
        }
        
        if (isWikiButtonPushed)
        {
            ChangeCanvas(titleCanvas, wikiCanvas);
            isWikiButtonPushed = false;
        }
        
        if (isBackToTitleButtonPushed)
        {
            ChangeCanvas(startCanvas, titleCanvas);
            isBackToTitleButtonPushed = false;
        }

        if (isWikiBackToTitleButtonPushed)
        {
            ChangeCanvas(wikiCanvas, titleCanvas);
            isWikiBackToTitleButtonPushed = false;
        }
    }

    // @brief キャンバスの切り替え
    // @param targetCanvas 非表示にするキャンバス
    private void ChangeCanvas(Canvas currentCanvas, Canvas targetCanvas)
    {
        currentCanvas.gameObject.SetActive(false);
        targetCanvas.gameObject.SetActive(true);
        backgroundAnimation.transform.position = new Vector3(-1, 0, 0);
    }
}