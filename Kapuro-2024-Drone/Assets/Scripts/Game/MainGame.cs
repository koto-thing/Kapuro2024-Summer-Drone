using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MainGame : MonoBehaviour
{
    [SerializeField] private AssetLoader assetLoader;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ButtonController buttonController;
    
    private void Start()
    {
        assetLoader.LoadAssetsAsync();
        playerController.Initialize();
        buttonController.Initialize();
    }

    private void Update()
    {
        playerController.PlayerUpdate();
    }
}
