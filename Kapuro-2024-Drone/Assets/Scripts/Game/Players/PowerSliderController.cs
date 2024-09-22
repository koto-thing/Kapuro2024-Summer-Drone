using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerSliderController : MonoBehaviour
{
    [SerializeField] private Slider powerSlider; // パワースライダー
    [SerializeField] private float speed; // パワースライダーの速度
    [SerializeField] private bool state; // パワースライダーの状態
    
    // @brief 初期化
    public void Initialize(float newSpeed)
    {
        powerSlider = GetComponent<Slider>();
        powerSlider.maxValue = 100;
        powerSlider.minValue = 0;
        powerSlider.value = 0;
        speed = newSpeed;
        state = false;
    }
    
    // @brief パワースライダーの値を設定
    public float MoveSlider()
    {
        switch (state)
        {
            case false:
                powerSlider.value += speed * Time.deltaTime;
                state = powerSlider.value >= powerSlider.maxValue;
                break;
            case true:
                powerSlider.value = 0;
                state = false;
                break;
        }

        if (Input.GetKeyUp(KeyCode.Space))
            return powerSlider.value;

        return -1;
    }
    
    // @brief パワースライダーのtransformをリセット
    public void ResetSlider()
    {
        powerSlider.value = 0;
        transform.DOLocalMove(new Vector3(0, -3.0f, 0), 1.0f);
        transform.DOLocalRotate(new Vector3(0, 0, 0), 1.0f);
    }
}