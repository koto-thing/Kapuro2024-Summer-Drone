using UnityEngine;

public abstract class AbstractPlayers : MonoBehaviour
{
    public enum PlayerState { Idle, DirectionSetting, PowerSetting, Move, Reset }

    private float speed;
    private Vector3 playerDirection;
    private float playerPower;
    private PlayerState state;
    private ArrowController arrowController;
    private bool isMoveable;
    
    public virtual PlayerState State { get; }
    public virtual bool IsOutofControl { get; set; }
    public virtual bool IsMoveable { get { return isMoveable; } set { isMoveable = value; } }
    public abstract void Initialize();
    public abstract void PlayerUpdate();
    public abstract void ChangeState();
    public abstract void DirectionSetting();
    public abstract void PowerSetting();
    public abstract void MoveDrone();
    public abstract void ResetParameters();
}