using System;
using DG.Tweening;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Transform player;  // プレイヤーの位置
    [SerializeField] private Transform target;  // 目的地の位置
    [SerializeField] private GameObject arrow;  // 矢印オブジェクト
    [SerializeField] private RectTransform arrowRectTransform;  // 矢印の RectTransform

    [SerializeField] private float arrowOffsetDistance = 50f;  // 矢印を Box から離す距離 (ピクセル)

    private void Start()
    {
        arrowRectTransform = arrow.GetComponent<RectTransform>();
        arrow.SetActive(false);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            arrow.SetActive(true);
            
            // プレイヤーと目的地の方向を Box を基準にして計算
            Vector3 direction = target.position - transform.position;

            // 矢印の角度を設定
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 矢印の角度を設定 (Box の位置から目的地を向く)
            arrowRectTransform.localRotation = Quaternion.Euler(0, 0, angle - 90);

            // 矢印の表示位置を Box の位置から少し離す (offset)
            // 方向ベクトルを正規化して、指定した距離分移動した位置に設定する
            Vector3 offsetDirection = direction.normalized * arrowOffsetDistance;

            // Box の位置 + オフセット分の方向に矢印を配置
            arrowRectTransform.position = transform.position + offsetDirection;

            // Box がプレイヤーに追従する処理
            gameObject.transform.DOMove(other.gameObject.transform.position, 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DronePort"))
        {
            Debug.Log("当たってます！");
            Destroy(gameObject);  // Box オブジェクトを破壊
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            arrow.SetActive(false);  // 矢印を非表示にする
        }
    }
}