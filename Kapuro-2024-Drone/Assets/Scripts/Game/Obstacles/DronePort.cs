using FMODUnity;
using UnityEngine;

public class DronePort : MonoBehaviour
{
    [Header("生成するオブジェクト")]
    [SerializeField] private GameObject target; //　生成するオブジェクト
    
    private void Start()
    {
        target.SetActive(false);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            transform.GetComponent<StudioEventEmitter>().Play();
            target.SetActive(true);
            Destroy(gameObject);
        }
    }
}