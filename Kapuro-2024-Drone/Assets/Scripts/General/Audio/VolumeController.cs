using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Header("VCA Path")]
    [SerializeField] public EventReference masterVCAPath;
    [SerializeField] public EventReference bgmVCAPath;
    [SerializeField] public EventReference sfxVCAPath;

    [Header("VCA Object")]
    [SerializeField] private FMOD.Studio.VCA masterVCA;
    [SerializeField] private FMOD.Studio.VCA bgmVCA;
    [SerializeField] private FMOD.Studio.VCA sfxVCA;

    [Header("Slider Object")] 
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    // @brief 初期化処理
    public void Initialize()
    {
        masterVCA = RuntimeManager.GetVCA(masterVCAPath.Path);
        bgmVCA = RuntimeManager.GetVCA(bgmVCAPath.Path);
        sfxVCA = RuntimeManager.GetVCA(sfxVCAPath.Path);
    }

    // @brief 音量の更新処理
    public void update()
    {
        if (SceneManager.GetSceneByName("Option").isLoaded) //Optionシーンの時のみ動作
        {
            masterVCA.setVolume(masterSlider.value);
            bgmVCA.setVolume(bgmSlider.value);
            sfxVCA.setVolume(sfxSlider.value);
        }
    }
}
