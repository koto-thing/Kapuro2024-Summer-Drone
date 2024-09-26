using Cinemachine;
using UnityEngine;

public class VirtualCameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera targetVirtualCamera = default; // 移動先の仮想カメラ
    private CinemachineVirtualCamera TargetVirtualCamera => targetVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera previousVirtualCamera; // 移動前の仮想カメラ

    [SerializeField] private CinemachineBrain cinemachineBrain; // メインカメラのCinemachineBrain

    private const int EnableVirtualCameraPriority = int.MaxValue;

    private void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    // @brief プレイヤーが自身の当たり判定に触れた時に呼ばれる
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") == false)
            return;
        
        DisableCurrentVirtualCamera();
        EnableTargetVirtualCamera();
    }

    // @brief 現在の仮想カメラを無効化
    private void DisableCurrentVirtualCamera()
    {
        var current = this.cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
        current.Priority = 0;
        previousVirtualCamera = current;
    }
    
    // @brief 移動先の仮想カメラを有効化
    private void EnableTargetVirtualCamera()
    {
        TargetVirtualCamera.enabled = true;
        TargetVirtualCamera.Priority = EnableVirtualCameraPriority;
        targetVirtualCamera = previousVirtualCamera; // 移動先の仮想カメラを移動前の仮想カメラに設定
    }
}
