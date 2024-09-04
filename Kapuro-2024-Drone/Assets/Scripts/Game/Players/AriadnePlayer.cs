using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class AriadnePlayer : AbstractPlayers
{
    [SerializeField] private PlayerState stateAriadne; // プレイヤーの状態
    [SerializeField] private float speedAriadne; // プレイヤーの速度
    [SerializeField] private Transform directionAriadne; // プレイヤーの方向
    [SerializeField] private float powerAriadne; // プレイヤーのパワー
    [SerializeField] private ArrowController arrowControllerAriadne; // 矢印のコントローラー
    [SerializeField] private PowerSliderController powerSliderControllerAriadne; // パワースライダーのコントローラー
    
    public override PlayerState State { get { return stateAriadne; } }

    // @brief プレイヤーの状態を取得(private)
    // @overload
    private void ChangeState(PlayerState nextState)
    {
        stateAriadne = nextState;
    }
    
    // @brief 初期化処理
    // @override
    public override void Initialize()
    {
        stateAriadne = PlayerState.Idle;
        speedAriadne = 1f;
        directionAriadne = null;
        powerAriadne = 0;
        arrowControllerAriadne.Initialize(100);
        powerSliderControllerAriadne.Initialize();
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
            powerSliderControllerAriadne.Initialize(); // パワースライダーの初期化
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
        Vector3 moveDirection = directionAriadne.up * (1 + powerAriadne / 100);
        transform.DOMove(transform.position + moveDirection * (powerAriadne + speedAriadne), 1.0f); // ドローンの移動
        transform.DORotate(new Vector3(0, 0, directionAriadne.eulerAngles.z), 1.0f); // ドローンの回転
        arrowControllerAriadne.ResetArrow(); // 矢印の初期化
        powerSliderControllerAriadne.ResetSlider(); // パワースライダーの初期化
        directionAriadne = null; // 矢印のTransformをnullにする
        powerAriadne = 0; // パワーを0にする
        ChangeState(PlayerState.Idle); // プレイヤーの状態を移行
    }
}