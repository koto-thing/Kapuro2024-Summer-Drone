using UnityEngine;

public class Main_StartUp : MonoBehaviour
{
    [SerializeField] private UIManager_StartUp uiManager;

    // @brief 初期化
    private void Start()
    {
        uiManager.Initialize();
    }

    // @brief 更新
    private void Update()
    {
        
    }
}