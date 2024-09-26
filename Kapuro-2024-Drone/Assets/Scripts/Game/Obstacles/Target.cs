using FMODUnity;
using UnityEngine;

public class Target : MonoBehaviour, IChangeable
{
    [SerializeField] private bool isCollided;
    [SerializeField] private FMODUnity.StudioEventEmitter studioEventEmitter;
    
    public bool IsCollided { get { return isCollided; } }

    private void Start()
    {
        studioEventEmitter = GetComponent<StudioEventEmitter>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollided = true;
            studioEventEmitter.Play();
            ChangeObjectStatus();
        }
    }
    
    public void ChangeObjectStatus()
    {
        // TODO: 画像の色を変えるなりする
        Destroy(gameObject);
    }
}
