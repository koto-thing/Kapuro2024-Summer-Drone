using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonController : MonoBehaviour, IBreakable
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.CompareTag("Player"))
            DestroyOnCollided();
    }
    
    public void DestroyOnCollided()
    {
        Destroy(gameObject);
    }
}
