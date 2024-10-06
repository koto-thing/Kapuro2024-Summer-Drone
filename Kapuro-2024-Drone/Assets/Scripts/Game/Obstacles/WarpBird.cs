using DG.Tweening;
using FMODUnity;
using UnityEngine;
using UnityEngine.Animations;

public class WarpBird : MonoBehaviour
{
    [Header("Moving settings")]
    [SerializeField] private bool isMoveable = false;
    [SerializeField] private bool isBerakable = false;
    [SerializeField] private Vector3[] waypoints;
    [SerializeField] private float duration = 5f;

    [Header("Warp settings")] 
    [SerializeField] private Vector3 target;
    
    [Header("Sprite")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private int currentWaypointIndex = 0;
    private Vector3 previousPosition;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<StudioEventEmitter>().Play();
            Collider2D playerCollider = other.transform.GetComponent<Collider2D>();

            if (playerCollider != null)
            {
                // Playerの当たり判定を無効化せず、トリガー化する
                playerCollider.isTrigger = true;

                // Playerと自身の移動を同期させる
                Sequence sequence = DOTween.Sequence();
                sequence.Append(other.transform.DOLocalMove(new Vector3(target.x, target.y, target.z), 5f))   // Playerを移動
                    .Join(transform.DOMove(target, 5f))          // 自身も移動
                    .AppendCallback(() => playerCollider.isTrigger = false)  // トリガーを元に戻す
                    .Append(spriteRenderer.DOFade(0, 1f))  // フェードアウト
                    .AppendCallback(() =>
                    {
                        if (isBerakable)
                        {
                            Destroy(gameObject);
                        }
                        else
                        {
                            transform.position = previousPosition;  // 初期位置に戻す
                        }
                    })
                    .Append(spriteRenderer.DOFade(1, 1f));  // フェードイン
            }
        }
    }
    
    private void Start()
    {
        previousPosition = transform.position;
        
        if(isMoveable)
            MoveAround();
    }

    private void MoveAround()
    {
        transform.DOPath(waypoints, duration, PathType.CatmullRom)
            .SetLoops(-1, LoopType.Yoyo) // 繰り返し移動
            .SetEase(Ease.Linear)
            .SetOptions(true, lockPosition: AxisConstraint.None, lockRotation: AxisConstraint.X | AxisConstraint.Y) // x, y 軸の回転をロック
            .SetLookAt(0.1f, Vector3.forward); // z 軸を基準に回転
    }
}