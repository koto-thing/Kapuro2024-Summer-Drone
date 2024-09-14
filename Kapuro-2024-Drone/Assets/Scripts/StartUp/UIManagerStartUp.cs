using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIManagerStartUp : MonoBehaviour
{
    [SerializeField] private FMODLogoController fmodLogoController;
    
    public async UniTask Initialize()
    {
        fmodLogoController.Initialize();

        await UniTask.CompletedTask;
    }
}
