using UnityEngine;

public abstract class AbstractPlayers : MonoBehaviour
{
    public enum PlayerState { Idle, DirectionSetting, PowerSetting, Move }

    private float speed;
    private Vector3 playerDirection;
    private float playerPower;
    private PlayerState state;
    private ArrowController arrowController;
    
    public virtual PlayerState State { get; }
    public abstract void Initialize();
    public abstract void ChangeState();
    public abstract void DirectionSetting();
    public abstract void PowerSetting();
    public abstract void MoveDrone();
}