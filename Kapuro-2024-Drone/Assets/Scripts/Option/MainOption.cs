using UnityEngine;

public class MainOption : MonoBehaviour
{
    [SerializeField] private AudioController audioController;
    [SerializeField] private ButtonController buttonController;

    // @brief 初期処理
    private void Start()
    {
        buttonController.Initialize(); // ボタンの初期処理
        audioController.Initialize(); // 音声の初期処理
    }
}
