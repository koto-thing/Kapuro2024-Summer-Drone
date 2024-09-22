using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class DialogueAnimater : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    
    public void Initialize()
    {
        
    }
    
    public void DialogueAnimaterUpdate()
    {
        
    }

    public void AnimateFirstDialogue()
    {
        backGround.transform.DOScale(new Vector3(2200, 400, 1), 0.5f).SetEase(Ease.InSine).OnComplete(async () =>
        {
            await backGround.transform.DOLocalMove(new Vector3(0, -340, 0), 0.5f).SetEase(Ease.InSine).AsyncWaitForCompletion();
        });
    }

    public void AnimateOnDestroy()
    {
        backGround.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InSine).OnComplete(async () =>
        {
            await backGround.transform.DOScale(new Vector3(0, 1, 1), 0.5f).SetEase(Ease.InSine).AsyncWaitForCompletion();
        });
    }
}