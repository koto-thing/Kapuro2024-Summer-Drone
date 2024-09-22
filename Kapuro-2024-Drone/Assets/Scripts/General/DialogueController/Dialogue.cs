using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private DialogueAnimater dialogueAnimater;
    
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI main;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private GameObject backGround;
    [SerializeField] private bool isButtonPushed = false;

    public TextMeshProUGUI Name { get { return name; } set { name.text = value.text ;} }
    public TextMeshProUGUI Main { get { return main; } set { main.text = value.text ;} }
    public bool IsButtonPushed { get { return isButtonPushed; } set { isButtonPushed = value; } }

    public void LoadDialogue()
    {
        gameObject.SetActive(true);
        
        name.gameObject.SetActive(false);
        main.gameObject.SetActive(false);
        image.gameObject.SetActive(false);

        backGround.transform.localScale = new Vector3(0, 400, 1);
        dialogueAnimater.AnimateFirstDialogue();
        
        name.gameObject.SetActive(true);
        main.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
    }
    
    public void DialogueUpdate()
    {
        ButtonWathcer();
    }

    public void DialogueDestroy()
    {
        name.gameObject.SetActive(false);
        main.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        
        dialogueAnimater.AnimateOnDestroy();
        gameObject.SetActive(false);
    }

    private void ButtonWathcer()
    {
        if (Input.GetKeyUp(KeyCode.Space) && gameObject.activeSelf)
            isButtonPushed = true;
    }
}