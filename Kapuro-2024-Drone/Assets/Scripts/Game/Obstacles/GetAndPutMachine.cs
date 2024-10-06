using UnityEngine;

public class PutAndGetMachine : MonoBehaviour
{
    [Header("Box")] 
    [SerializeField] private GameObject box;
    [SerializeField] private Vector3 boxPosition;

    [Header("DronePort")] 
    [SerializeField] private GameObject dronePort;
    [SerializeField] private Vector3 dronePortPosition;

    private void Start()
    {
        box.transform.position = boxPosition;
        dronePort.transform.position = dronePortPosition;
    }
}
