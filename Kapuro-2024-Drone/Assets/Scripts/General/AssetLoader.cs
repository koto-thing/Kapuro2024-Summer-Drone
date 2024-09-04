using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetLoader : MonoBehaviour
{
    [SerializeField] private List<AssetReference> assetReferences2D;

    public async void LoadAssetsAsync()
    {
        foreach(var assetReference in assetReferences2D)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var spriteHandle = Addressables.LoadAssetAsync<Sprite>(assetReference);
            var sprite = await spriteHandle.ToUniTask();

            spriteRenderer.sprite = sprite;
        }
    }
}