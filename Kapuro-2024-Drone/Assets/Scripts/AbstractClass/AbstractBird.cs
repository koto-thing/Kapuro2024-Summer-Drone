using UnityEngine;

public abstract class AbstractBird : MonoBehaviour
{

    private float birdSpeed;
    private float birdPower;
    private Vector3 birdDirection;
    
    public abstract void Initialize();
    public abstract void BirdUpdate();
    public virtual void SetParameter(Vector3 birdPosition) { }
    public virtual void SetParameter(float speed, float birdPower, Vector3 birdDirection) {}
}