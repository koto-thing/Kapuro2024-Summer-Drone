using System;
using DG.Tweening;
using UnityEngine;

public class SmallBird : AbstractBird
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float power = 100;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float lifetime = 15f;

    private float timer = 0f;
    private ObstacleFactryController obstacleFactoryController;
    private Tween moveTween;

    public override void Initialize()
    {
        obstacleFactoryController = FindObjectOfType<ObstacleFactryController>();
    }

    public override void BirdUpdate()
    {
        Move();
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    public override void SetParameter(Vector3 birdDirection)
    {
        this.direction = birdDirection;
    }

    private void Move()
    {
        Vector3 destination = transform.position + direction.normalized * power;

        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        moveTween = transform.DOMove(destination, speed).OnKill(() => moveTween = null);
    }

    private void OnBecameInvisible()
    {
        if (moveTween != null) moveTween.Kill();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (moveTween != null) moveTween.Kill();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (obstacleFactoryController != null)
        {
            obstacleFactoryController.RemoveBird(this);
        }
    }
}