using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IChangeable
{
    [SerializeField] private bool isCollided;
    
    public bool IsCollided { get { return isCollided; } }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCollided = true;
        
        if(collision.gameObject.CompareTag("Player"))
            ChangeObjectStatus();
    }
    
    public void ChangeObjectStatus()
    {
        // TODO: 画像の色を変えるなりする
        Destroy(gameObject);
    }
}
