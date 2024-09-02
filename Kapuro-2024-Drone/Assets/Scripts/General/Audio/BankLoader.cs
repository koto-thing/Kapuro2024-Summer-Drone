using System.Collections.Generic;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class BankLoader : MonoBehaviour
{
    [Header("Audio Controller")]
    [SerializeField] private AudioController audioController;
    
    [Header("Bank Loader Objects")]
    [SerializeField] private List<StudioBankLoader> studioBankLoaders;

    // @brief 初期化処理
    public void Initialize()
    {
        for (int i = 0; i < audioController.AudioSources.Count; i++)
            studioBankLoaders[i] = audioController.AudioSources[i].GetComponent<StudioBankLoader>(); // AudioSourcesからStudioBankLoaderを取得
    }

    // @brief バンクをロード
    // @param index 配列番号
    public void LoadBank(int index)
    {
        studioBankLoaders[index].SendMessage("Load");
    }

    // @brief バンクをアンロード
    // @param index 配列番号
    public void UnloadBank(int index)
    {
        studioBankLoaders[index].SendMessage("Unload");
    }
}