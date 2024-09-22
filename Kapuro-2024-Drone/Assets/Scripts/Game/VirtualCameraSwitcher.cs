using Cinemachine;
using UnityEngine;

public class VirtualCameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera targetVirtualCamera = default;
    private CinemachineVirtualCamera TargetVirtualCamera => targetVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera previousVirtualCamera;

    [SerializeField] private CinemachineBrain cinemachineBrain;

    private const int EnableVirtualCameraPriority = int.MaxValue;

    private void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") == false)
            return;
        
        Debug.Log("here");
        DisableCurrentVirtualCamera();
        EnableTargetVirtualCamera();
    }

    private void DisableCurrentVirtualCamera()
    {
        var current = this.cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
        current.Priority = 0;
        previousVirtualCamera = current;
    }
    
    private void EnableTargetVirtualCamera()
    {
        TargetVirtualCamera.enabled = true;
        TargetVirtualCamera.Priority = EnableVirtualCameraPriority;
        targetVirtualCamera = previousVirtualCamera;
    }
}
