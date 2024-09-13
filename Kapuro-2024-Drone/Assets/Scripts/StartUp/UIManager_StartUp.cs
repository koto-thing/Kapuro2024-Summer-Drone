using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_StartUp : MonoBehaviour
{
    [SerializeField] private FMODLogoController fmodLogoController;
    
    public void Initialize()
    {
        fmodLogoController.Initialize();
    }
}
