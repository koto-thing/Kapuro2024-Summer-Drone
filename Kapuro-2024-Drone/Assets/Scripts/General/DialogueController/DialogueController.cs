using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [Header("CSVReader")]
    [SerializeField] private CSVReader csvReader;
    [SerializeField] private int currentStringIndex = 0;
    
    [Header("Dialogue")]
    [SerializeField] private Dialogue dialogue;

    [Header("Flag")]
    [SerializeField] private bool isDialogueFinished;

    [SerializeField] private int currentBoolIndex = 0;
    public bool IsFirstDialogueFinished { get { return isDialogueFinished; } }

    private const int NAMECOL = 0;
    private const int TEXTCOL = 1;
    
    // @brief 初期化
    public async UniTask Initialize()
    {
        csvReader.LoadCSV();
        dialogue.LoadDialogue();
        
        SetDialogue();
        
        await UniTask.CompletedTask;
    }

    // @brief 次のダイアログをロード
    public void LoadDialogue()
    {
        if (isDialogueFinished)
        {
            isDialogueFinished = false;
            csvReader.LoadCSV();
            dialogue.LoadDialogue();
            SetDialogue();
        }
    }
    
    // @brief ダイアログの更新
    public void DialogueControllerUpdate()
    {
        dialogue.DialogueUpdate();
        
        SetNextDialogue();
    }
    
    // @brief ダイアログをセット
    // @overLoad
    private void SetDialogue()
    {
        dialogue.Name.text = csvReader.GetCSVData(currentStringIndex, NAMECOL);
        dialogue.Main.text = csvReader.GetCSVData(currentStringIndex, TEXTCOL);
        
        currentStringIndex++;
        dialogue.IsButtonPushed = false;
    }

    // @brief ダイアログをセット
    // @overLoad
    private void SetDialogue(String name, String text)
    {
        dialogue.Name.text = name;
        dialogue.Main.text = text;
        
        currentStringIndex++;
        dialogue.IsButtonPushed = false;
    }
    
    // @brief 次のダイアログをセット
    private void SetNextDialogue()
    {
        if (dialogue.IsButtonPushed && currentStringIndex < csvReader.LastRow)
        {
            SetDialogue();
        }
        else if(dialogue.IsButtonPushed && currentStringIndex == csvReader.LastRow)
        {
            SetDialogue("", "(Press the button to proceed...)");
        }
        else if(dialogue.IsButtonPushed && currentStringIndex > csvReader.LastRow)
        {
            currentStringIndex = 0;
            dialogue.DialogueDestroy();
            isDialogueFinished = true;
        }
    }
}