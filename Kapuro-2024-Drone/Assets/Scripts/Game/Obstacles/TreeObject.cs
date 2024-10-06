using FMODUnity;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<StudioEventEmitter>().Play();
        }
    }
}