using UnityEngine;

public class MainTitle : MonoBehaviour
{
    [SerializeField] private AudioController audioController;
    [SerializeField] private ButtonController buttonController;

    private void Start()
    {
        buttonController.Initialize(); // ボタンの初期処理
        audioController.Initialize(); // 音声の初期処理
    }
}