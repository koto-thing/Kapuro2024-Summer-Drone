using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class FMODLogoController : MonoBehaviour
{
    // @brief 初期化
    public void Initialize()
    {
        SpriteRenderer FMODlogo = gameObject.GetComponent<SpriteRenderer>();

        gameObject.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 1.0f).SetEase(Ease.InSine).OnComplete(SetNexScene);
        FMODlogo.DOFade(1.0f, 1.0f);
    }
    
    // @brief 次のシーンへ遷移
    private void SetNexScene()
    {
        UniTask.WaitForSeconds(1.5f).ContinueWith(() => SceneController.LoadScene("Title")).Forget();
    }
}
