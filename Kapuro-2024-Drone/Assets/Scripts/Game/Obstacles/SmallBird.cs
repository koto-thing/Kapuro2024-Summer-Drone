using System;
using DG.Tweening;
using UnityEngine;

public class SmallBird : AbstractBird
{
    [Header("BirdSprite")]
    [SerializeField] private SpriteRenderer birdSprite;
    
    [Header("BirdParameter")]
    [SerializeField] private float speed = 1;
    [SerializeField] private float power = 100;
    [SerializeField] private Vector3 direction; // 鳥の向き 
    [SerializeField] private float lifetime = 15f;

    [Header("Timer")]
    [SerializeField] private float timer = 0f;
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
            birdSprite.DOFade(0, 1f).OnComplete(() => Destroy(gameObject));
    }

    public override void SetParameter(Vector3 birdDirection)
    {
        // 指定された方向を設定
        this.direction = birdDirection.normalized;

        // 鳥の向きを指定された方向に合わせて回転
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Move()
    {
        // 設定された direction に基づいて移動方向を計算
        Vector3 destination = transform.position + direction * power;

        // 既にアクティブな Tween がある場合は停止する
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        // 新しい移動 Tween を設定し、停止時の処理を追加
        moveTween = transform.DOMove(destination, speed).OnKill(() => moveTween = null);
    }
    
    private void MoveByWind(Quaternion windDirection, float windPower)
    {
        // 風の方向を計算
        float windAngleInRadians = (windDirection.eulerAngles.z + 90) * Mathf.Deg2Rad;
    
        // 風の方向ベクトルを計算（2D空間の向き）
        Vector2 moveDirection2D = new Vector2(Mathf.Cos(windAngleInRadians), Mathf.Sin(windAngleInRadians)) * (1 + windPower / 100);

        // 2D座標での目的地を計算し、z座標は元の位置を維持
        Vector3 movePosition = new Vector3(
            transform.position.x + moveDirection2D.x * (2 * (windPower + speed)),
            transform.position.y + moveDirection2D.y * (2 * (windPower + speed)),
            transform.position.z  // z座標を維持
        );

        // 移動と回転のアニメーションをシーケンスで管理
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.Append(transform.DOMove(movePosition, 1.0f));
        moveSequence.Join(transform.DORotate(new Vector3(0, 0, windDirection.eulerAngles.z), 1.0f));
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
        
        switch (other.gameObject.tag)
        {
            case "Player":
                if(moveTween != null)
                    moveTween.Kill();
                Destroy(gameObject);
                break;
            case "Wind":
                if(moveTween != null)
                    moveTween.Kill();
                MoveByWind(other.gameObject.GetComponent<Wind>().WindDirection, other.gameObject.GetComponent<Wind>().WindPower);
                break;
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
