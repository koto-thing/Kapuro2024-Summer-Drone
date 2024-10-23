using System;
using DG.Tweening;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DroneLostDetecter : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float timer;
    [SerializeField] private float startTime = 3.0f;
    [SerializeField] private float showTime;
    [SerializeField] private GameObject lostPopUp;
    [SerializeField] private GameObject lostPopUpSprite;
    [SerializeField] private TextMeshProUGUI lostText;
    
    [Header("Drone")]
    [SerializeField] private Vector3 dronePosition;
    [SerializeField] private Vector3 droneDirection;
    
    [Header("Flag")]
    [SerializeField] private bool isDroneLost = false;
    
    [Header("FMOD")]
    [SerializeField] private StudioEventEmitter studioEventEmitter;
    
    public void Initialize()
    {
        lostPopUp.SetActive(false);
    }

    public void DroneLostDetecterUpdate()
    {
        // ドローンが見失われた時のタイマー
        if (isDroneLost)
        {
            timer += Time.deltaTime;
            showTime = Mathf.Clamp(startTime - timer, 0f, startTime);
        
            lostText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                (int)showTime / 60,
                (int)showTime % 60,
                (int)(showTime * 100) % 60
            );

            studioEventEmitter.SetParameter("BGM01Param", Mathf.Clamp(timer, 0, 3), false);
        }
        
        // ドローンが見失われた時の位置に戻す
        if(isDroneLost && showTime <= 0f)
        {
            lostText.text = "Automatic return program in progress...";
            GetComponent<AbstractPlayers>().IsMoveable = false;
            gameObject.transform.DORotate(droneDirection, 2.0f, RotateMode.Fast);
            gameObject.transform.DOMove(dronePosition, 2.0f)
                .OnComplete(() => { isDroneLost = false; GetComponent<AbstractPlayers>().IsMoveable = true; });
            lostPopUpSprite.GetComponent<SpriteRenderer>().DOFade(0.0f, 0.5f)
                .OnComplete(() => { lostPopUp.SetActive(false);});
        }

        if (isDroneLost == false)
        {
            if (timer > 0) timer = 0;
            studioEventEmitter.SetParameter("BGM01Param", timer, false);
        }
    }
    
    // @brief ドローンが画面内に出たの処理
    public void OnBecameVisible()
    {
        isDroneLost = false;
        timer = 0;
        lostPopUpSprite.GetComponent<SpriteRenderer>().DOFade(0.0f, 0.5f)
            .OnComplete(() => { lostPopUp.SetActive(false); });
    }
    
    // @brief ドローンが見失われた時の処理
    public void OnBecomeUnJamming()
    {
        isDroneLost = false;
        timer = 0;
        lostPopUpSprite.GetComponent<SpriteRenderer>().DOFade(0.0f, 0.5f)
            .OnComplete(() => { lostPopUp.SetActive(false); });
    }

    // @brief ドローンが見失われた時の処理
    public void OnBecameInvisible()
    {
        // ドローンが最初に見失われた時の位置を記録
        if (isDroneLost == false)
        {
            dronePosition = gameObject.transform.position;
            isDroneLost = true;
        }
        
        FixDronePosition();
        
        lostPopUp.SetActive(true);
        lostPopUpSprite.GetComponent<SpriteRenderer>().DOFade(0.8f, 1f);
    }
    
    // @brief ドローンが見失われた時の処理
    public void OnBecomeJamming()
    {
        // ドローンが最初に見失われた時の位置を記録
        if (isDroneLost == false)
        {
            dronePosition = gameObject.transform.position;
            timer = 0;
            isDroneLost = true;
        }
        
        FixDronePosition();
        
        lostPopUp.SetActive(true);
        lostPopUpSprite.GetComponent<SpriteRenderer>().DOFade(0.8f, 1f);
    }

    private void FixDronePosition()
    {
        if (Camera.main == null)
            return; // nullの場合は処理を中止
        
        // カメラのビューポートのワールド座標を取得
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        
        // カメラの方向に向けるためのベクトルを計算
        Vector3 direction = Camera.main.transform.position - transform.position;

        // Z軸の回転角度を計算するために atan2 を使用
        float targetZAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 現在のローカル回転角度を取得して、Z軸のみ変更
        droneDirection = new Vector3(0, 0, targetZAngle - 90);

        
        // ドローンが画面下部に出たときの微調整
        if (dronePosition.y < bottomLeft.y)
        {
            dronePosition.y = bottomLeft.y + 100;
        }
        
        // ドローンが画面右側に出たときの微調整
        if (dronePosition.x > topRight.x)
        {
            dronePosition.x = topRight.x - 100;
        }
        
        // ドローンが画面左側に出たときの微調整
        if (dronePosition.x < bottomLeft.x)
        {
            dronePosition.x = bottomLeft.x + 100;
        }
        
        // ドローンが画面上部に出たときの微調整
        if (dronePosition.y > topRight.y)
        {
            dronePosition.y = topRight.y - 100;
        }
    }
}