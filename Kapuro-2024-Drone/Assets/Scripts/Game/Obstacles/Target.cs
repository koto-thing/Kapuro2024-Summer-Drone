using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Target : MonoBehaviour, IChangeable
{
    [SerializeField] private bool isCollided;
    [SerializeField] private StudioEventEmitter studioEventEmitter;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<ParticleSystem> particleSystems;
    
    public bool IsCollided { get { return isCollided; } }

    private void Start()
    {
        studioEventEmitter = GetComponent<StudioEventEmitter>();
    }

    private void Update()
    {
        ChangeObjectStatus();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollided = true;
            foreach (var particleSystem in particleSystems)
                particleSystem.Play();
            studioEventEmitter.Play();
            GetComponent<Collider2D>().enabled = false;
            spriteRenderer.enabled = false;
        }
    }
    
    public void ChangeObjectStatus()
    {
        if (AllParticlesStopped() && isCollided) // IsAlive() を使用
        {
            Destroy(gameObject, 0.1f);
        }
    }
    
    private bool AllParticlesStopped()
    {
        foreach (var particleSystem in particleSystems)
        {
            // パーティクルシステムが null または Destroy されていないかを確認する
            if (particleSystem != null && particleSystem.IsAlive()) 
            {
                return false; // まだ生きているパーティクルがあれば false を返す
            }
        }
        return true; // 全てのパーティクルが停止していれば true
    }

}
