using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public AbstractGameMain mainObject;

    [Header("GameのPrefab")] 
    [SerializeField] private List<GameObject> gamePrefabs;

    private void Awake()
    {
        switch (GameInfo.currentGameType)
        {
            case GameInfo.GameType.Tutorial:
                Instantiate( gamePrefabs.Find(prefab => prefab.name == "Main(Tutorial)") );
                break;
            case GameInfo.GameType.Mission01:
                Instantiate( gamePrefabs.Find(prefab => prefab.name == "Main(Mission01)") );
                break;
        }
        
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