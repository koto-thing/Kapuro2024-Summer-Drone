using DG.Tweening;
using UnityEngine;

public class BigBirdWithTree : MonoBehaviour
{
    [Header("Big Bird")] 
    [SerializeField] private GameObject bigBird;

    private Vector3 previousPosition;
    private Tween moveTween;
    
    private void Start()
    {
        previousPosition = bigBird.transform.position;
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moveTween.Kill();
            moveTween = bigBird.transform.DOMove(other.transform.position, 10f);
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moveTween.Kill();
            moveTween = bigBird.transform.DOMove(previousPosition, 5f)
                .OnComplete(() => bigBird.transform.position = previousPosition);
        }
    }
}