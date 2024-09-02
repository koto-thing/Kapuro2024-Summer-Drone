using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Audio Source Object")]
    [SerializeField] public List<GameObject> audioSources;
    public List<GameObject> AudioSources { get; set; }
    
    [Header("Controller Relational Objects")]
    [SerializeField] private ParameterController parameterController;
    [SerializeField] private BankLoader bankLoader;
    [SerializeField] private VolumeController audioVolumeController;
    
    // @brief 初期化処理
    public void Initialize()
    {
        AudioSources = audioSources;
        parameterController.Initialize(); // パラメータの初期処理
        bankLoader.Initialize(); // バンクの初期処理
        audioVolumeController.Initialize(); // 音量の初期処理
    }

    public void update()
    {
        audioVolumeController.update();
    }
    
    // @brief 各種プロパティ
    public ParameterController ParameterController { get; set; }
    public BankLoader BankLoader { get; set; }
    public VolumeController VolumeController { get; set; }
}