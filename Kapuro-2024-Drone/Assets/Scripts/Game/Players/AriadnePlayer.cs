using System;
using DG.Tweening;
using UnityEngine;

public class AriadnePlayer : AbstractPlayers
{
    [SerializeField] private PlayerState stateAriadne; // プレイヤーの状態
    [SerializeField] private float speedAriadne; // プレイヤーの速度
    [SerializeField] private float powerAriadne; // プレイヤーのパワー
    [SerializeField] private Transform directionAriadne; // プレイヤーの方向
    [SerializeField] private ArrowController arrowControllerAriadne; // 矢印のコントローラー
    [SerializeField] private PowerSliderController powerSliderControllerAriadne; // パワースライダーのコントローラー
    [SerializeField] private DroneLostDetecter droneLostDetecter; // ドローンの見失い検知

    [SerializeField] private Vector3 moveDirection; // ドローンの移動方向
    [SerializeField] private Vector3 movePosition; // ドローンの移動位置
    private Tween moveTween; // ドローンの移動タスク
    private Tween rotateTween; // ドローンの回転タスク
    
    public override PlayerState State { get { return stateAriadne; } }

    // @brief プレイヤーの状態を取得(private)
    // @overload
    private void ChangeState(PlayerState nextState)
    {
        stateAriadne = nextState;
    }
    
    // @brief プレイヤーの衝突判定
    // @param collision 衝突したオブジェクト
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit " + collision.gameObject.tag);
        switch(collision.gameObject.tag)
        {
            case "Boundable":
                KillMoveTween(); // ドローンの移動タスクをキャンセル
                MoveReflected(collision); // ドローンの移動反射
                break;
            
            case "Breakable":
                Debug.Log("Hit Breakable");
                break;
            
            case "Target":
                Debug.Log("Hit Target");
                break;
            
            case "Tree":
                Debug.Log("Hit Tree");
                KillMoveTween(); // ドローンの移動タスクをキャンセル
                break;
            
            case "SmallBird":
                Debug.Log("Hit SmallBird");
                KillMoveTween(); // ドローンの移動タスクをキャンセル
                MoveRandom(); // ドローンのランダム移動
                break;
            
            case "Block":
                Debug.Log("Hit Block");
                KillMoveTween(); // ドローンの移動タスクをキャンセル
                break;
            
            case "Wind":
                Debug.Log("Hit Wind");
                Wind wind = collision.gameObject.GetComponent<Wind>(); // Windコンポーネントを取得
                KillMoveTween(); // ドローンの移動タスクをキャンセル
                MoveByWind(wind.WindDirection, wind.WindPower); // ドローンの風による移動
                break;
            
            case "SpecialBird":
                Debug.Log("Hit SpecialBird");
                KillMoveTween(); // ドローンの移動タスクをキャンセル
                MoveRandom(); // ドローンのランダム移動
                break;
        }
    }
    
    // @brief プレイヤーの衝突判定
    private void OnCollisionStay2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Jammer":
                droneLostDetecter.OnBecomeJamming();
                break;
        }
    }
    
    // @brief プレイヤーの衝突判定
    private void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Jammer":
                droneLostDetecter.OnBecomeUnJamming();
                break;
        }
    }

    // @brief ドローンの移動タスクをキャンセル
    private void KillMoveTween()
    {
        if (moveTween != null && rotateTween != null)
        {
            moveTween.Kill();
            rotateTween.Kill();
            moveTween = null;
            rotateTween = null;
        }
    }
    
    // @brief 初期化処理
    // @override
    public override void Initialize()
    {
        stateAriadne = PlayerState.Idle;
        speedAriadne = 150f;
        powerAriadne = 0;
        directionAriadne = null;
        arrowControllerAriadne.Initialize(300);
        powerSliderControllerAriadne.Initialize(100f);
        droneLostDetecter.Initialize();
    }
    
    // @brief プレイヤーの更新処理
    // @override
    public override void PlayerUpdate()
    {
        droneLostDetecter.DroneLostDetecterUpdate();
    }

    // @brief ドローンの状態変更(public)
    // @override
    public override void ChangeState()
    {
        if(Input.GetKeyUp(KeyCode.Space))
            stateAriadne = PlayerState.PowerSetting;
    }
    
    // @brief ドローンのパワー設定
    // @override
    public override void PowerSetting()
    {
        powerAriadne = powerSliderControllerAriadne.MoveSlider(); // パワースライダーの値を取得
        if (powerAriadne != -1) // パワースライダーの値が-1でない場合
        {
            ChangeState(PlayerState.DirectionSetting); // プレイヤーの状態を移行
            //powerSliderControllerAriadne.Initialize(); // パワースライダーの初期化
        }
    }
    
    // @brief ドローンの方向設定
    // @override
    public override void DirectionSetting()
    {
        directionAriadne = arrowControllerAriadne.MoveArrow(transform.position); // 矢印の移動
        if (directionAriadne != null) //矢印のTransformがnullでない場合
        {
            ChangeState(PlayerState.Move); // プレイヤーの状態を移行
        }
    }
    
    // @brief ドローンの移動
    // @override
    public override void MoveDrone()
    {
        if (directionAriadne == null)
            ChangeState(PlayerState.Reset); // directionAriadneがnullの場合、プレイヤーの状態を移行
        
        moveDirection = directionAriadne.up.normalized * (1 + powerAriadne / 100);
        movePosition = transform.position + moveDirection.normalized * (2 * (powerAriadne + speedAriadne) ); // 移動先位置を計算
        
        moveTween = transform.DOMove(movePosition, 1.0f); // ドローンの移動
        rotateTween = transform.DORotate(new Vector3(0, 0, directionAriadne.eulerAngles.z), 1.0f).OnComplete(OnCompleteMoveTask); // ドローンの回転
        
        ChangeState(PlayerState.Reset); // プレイヤーの状態を移行
    }
    
    public override void ResetParameters()
    {
        arrowControllerAriadne.ResetArrow();
        powerSliderControllerAriadne.ResetSlider();
        powerAriadne = 0;
        ChangeState(PlayerState.Idle);
    }
    
    // @brief ドローンのランダム移動
    private void MoveRandom()
    {
        // 移動量をランダムに決定
        float x = UnityEngine.Random.Range(-100f, 100f);
        float y = UnityEngine.Random.Range(-100f, 100f);
        float z = UnityEngine.Random.Range(0, 360f);
        movePosition = new Vector3(transform.position.x + x, transform.position.y + y, 0);
        moveDirection = new Vector3(0, 0, z);
        transform.DOMove(movePosition, 1.0f);
        transform.DORotate(moveDirection, 1.0f)
            .SetEase(Ease.InOutSine);
    }

    // @brief ドローンの風による移動
    // @param Quaternion windDirection 風の向き, float windPower 風の強さ
    private void MoveByWind(Quaternion windDirection, float windPower)
    {
        // 2D向けに風の向きを取得（z軸の回転角度を使用）
        float windAngleInRadians = (windDirection.eulerAngles.z + 90) * Mathf.Deg2Rad;
    
        // 風の方向ベクトルを計算（2D空間の向き）
        Vector2 moveDirection2D = new Vector2(Mathf.Cos(windAngleInRadians), Mathf.Sin(windAngleInRadians)) * (1 + windPower / 100);

        // 2D座標での目的地を計算し、z座標は元の位置を維持
        Vector3 movePosition = new Vector3(
            transform.position.x + moveDirection2D.x * (2 * (windPower + speedAriadne)),
            transform.position.y + moveDirection2D.y * (2 * (windPower + speedAriadne)),
            transform.position.z  // z座標を維持
        );
        Sequence moveSequence = DOTween.Sequence(); // 移動と回転のアニメーションをシーケンスで管理
        
        moveSequence.Append(transform.DOMove(movePosition, 1.0f));
        moveSequence.Join(transform.DORotate(new Vector3(0, 0, windDirection.eulerAngles.z), 1.0f));
        moveSequence.OnComplete(OnCompleteMoveTask);
    }
    
    // @brief ドローンの移動反射
    // @param collision 衝突したオブジェクト
    private void MoveReflected(Collision2D collision)
    {
        Vector2 collisionNormal = collision.contacts[0].normal; // 衝突した表面の法線ベクトルを取得
        moveDirection = Vector3.Reflect(moveDirection.normalized, collisionNormal); // 移動方向ベクトルを反射させる
        movePosition = transform.position + moveDirection.normalized * (2 * (powerAriadne + speedAriadne)); // 反射後の移動先位置を計算
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg; // 新しい反射後の方向に基づいて回転角度を計算
        
        moveTween = transform.DOMove(movePosition, 1.0f); // ドローンの移動
        rotateTween = transform.DORotate(new Vector3(0, 0, angle), 1.0f).OnComplete(OnCompleteMoveTask); // ドローンの回転
        
        arrowControllerAriadne.ResetArrow(); // 矢印の初期化
        powerSliderControllerAriadne.ResetSlider(); // パワースライダーの初期化
        powerAriadne = 0; // パワーを0にする
        ChangeState(PlayerState.Idle); // プレイヤーの状態を移行
    }
    
    // @brief ドローンの移動完了時の処理
    private void OnCompleteMoveTask()
    {
        directionAriadne = null; // 矢印のTransformをnullにする
    }
}