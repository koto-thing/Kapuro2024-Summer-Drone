using DG.Tweening;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float speed; // 矢印の速度
    [SerializeField] private Transform direction; // 矢印の方向
    [SerializeField] private bool isMove; // 矢印が動いているかどうか
    
    // @brief 初期化
    // @param newSpeed 矢印の回転速度
    public void Initialize(float newSpeed)
    {
        speed = newSpeed;
        direction = transform;
        isMove = false;
    }

    // @brief 矢印の回転と方向の決定
    // @param centerPoint 矢印の中心点
    public Transform MoveArrow(Vector3 centerPoint)
    {
        if (Input.GetKeyUp(KeyCode.Space) && isMove == true)
        {
            return transform;
        }
        else
        {
            isMove = true;
            transform.RotateAround(centerPoint, Vector3.forward, speed * Time.deltaTime);
        }
        
        return null;
    }
    
    // @brief 矢印のtransformをリセット
    public void ResetArrow()
    {
        isMove = false;
        transform.DOLocalMove(new Vector3(0, 3.5f, 0), 1.0f);
        transform.DOLocalRotate(new Vector3(0, 0, 0), 1.0f);
    }
}