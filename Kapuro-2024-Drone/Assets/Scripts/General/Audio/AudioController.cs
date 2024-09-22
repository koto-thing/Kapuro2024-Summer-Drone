using System;
using Cysharp.Threading.Tasks;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioController : SingletonWithMonoBehaviour<AudioController>
{
    private readonly String masterVCAPath = "vca:/Master";
    private readonly String bgmVCAPath = "vca:/BGM_VCA";
    private readonly String sfxVCAPath = "vca:/SFX_VCA";
    
    [Header("VCA Object")]
    [SerializeField] private FMOD.Studio.VCA masterVCA;
    [SerializeField] private FMOD.Studio.VCA bgmVCA;
    [SerializeField] private FMOD.Studio.VCA sfxVCA;
    
    [Header("Slider Object")] 
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    
    [Header("Volume")]
    [SerializeField] private float masterVolume;
    [SerializeField] private float bgmVolume;
    [SerializeField] private float sfxVolume;

    private bool isInitialized = false;

    // @brief Sliderの初期化処理
    public async UniTask SetSlider()
    {
        if (SceneManager.GetSceneByName("Option").isLoaded && !isInitialized)
        {
            masterSlider = GameObject.Find("MasterSlider").GetComponent<Slider>();
            bgmSlider = GameObject.Find("BGMSlider").GetComponent<Slider>();
            sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
            
            isInitialized = true;
        }
        else
        {
            masterSlider = GameObject.Find("MasterSlider").GetComponent<Slider>();
            bgmSlider = GameObject.Find("BGMSlider").GetComponent<Slider>();
            sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
            masterSlider.value = masterVolume;
            bgmSlider.value = bgmVolume;
            sfxSlider.value = sfxVolume;
        }
        
        await UniTask.CompletedTask;
    }

    // @brief 初期化処理
    protected override void OnAwakeProcess()
    {
        DontDestroyOnLoad(this);
        masterVCA = RuntimeManager.GetVCA(masterVCAPath);
        bgmVCA = RuntimeManager.GetVCA(bgmVCAPath);
        sfxVCA = RuntimeManager.GetVCA(sfxVCAPath);
    }

    // @brief 更新処理
    public void AudioControllerUpdate()
    {
        if (SceneManager.GetSceneByName("Option").isLoaded) //Optionシーンの時のみ動作
        {
            masterVCA.setVolume(masterVolume = masterSlider.value);
            bgmVCA.setVolume(bgmVolume = bgmSlider.value);
            sfxVCA.setVolume(sfxVolume = sfxSlider.value);
        }
    }
}