using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public AbstractGameMain mainObject;

    private void Awake()
    {
        mainObject = FindObjectsByType<AbstractGameMain>(FindObjectsSortMode.None)[0];
    }
    
    private void Start()
    {
        mainObject.Initialize();
    }

    private void Update()
    {
        mainObject.MainUpdate();
    }
}