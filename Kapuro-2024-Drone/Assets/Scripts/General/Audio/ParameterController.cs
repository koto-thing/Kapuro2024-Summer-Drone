using System.Collections.Generic;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class ParameterController : MonoBehaviour
{
    [Header("Audio Controller")] 
    [SerializeField] private AudioController audioController;
    
    [Header("Component Object")]
    [SerializeField] private List<StudioEventEmitter> studioEventEmitters;

    // @brief 初期化処理
    public void Initialize()
    {
        for (int i = 0; i < audioController.AudioSources.Count; i++)
            studioEventEmitters[i] = audioController.AudioSources[i].GetComponent<StudioEventEmitter>(); // AudioSourcesからStudioEventEmitterを取得
    }

    // @brief オーディオを再生
    // @param index 配列番号
    public void PlayAudio(int index)
    {
        studioEventEmitters[index].SendMessage("Play");
    }

    // @brief オーディオを停止
    // @param index 配列番号
    public void StopAudio(int index)
    {
        studioEventEmitters[index].SendMessage("Stop");
    }
    
    // @brief パラメータを設定
    // @param index 配列番号、 parameterName パラメータ名、 value パラメータ値
    public void SetParameter(int index, string parameterName, float value)
    {
        studioEventEmitters[index].SetParameter(parameterName, value);
    }
}