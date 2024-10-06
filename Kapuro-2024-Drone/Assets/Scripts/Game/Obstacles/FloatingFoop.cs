using DG.Tweening;
using FMODUnity;
using UnityEngine;

public class FloatingFoop : MonoBehaviour
{
    [SerializeField] private bool isMoveable = false;
    [SerializeField] private Vector3[] waypoints;
    [SerializeField] private float duration = 5f;

    private int currentWaypointIndex = 0;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            gameObject.GetComponent<StudioEventEmitter>().Play();
    }

    private void Start()
    {
        if(isMoveable)
            MoveAround();
    }

    private void MoveAround()
    {
        transform.DOPath(waypoints, duration, PathType.CatmullRom)
            .SetLoops(-1, LoopType.Yoyo)  // 繰り返し移動
            .SetEase(Ease.Linear);
    }
}