using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    //Panels
    [SerializeField] private List<GameObject> panelList;
    
    [SerializeField] private int currentPanelIndex = 0;

    private void Start()
    {
        for(int i = 1 ; i < panelList.Count ; i++)
        {
            panelList[i].SetActive(false);
        }
    }

    public void SetNextPanel()
    {
        Debug.Log("ShowNextPanel");
        panelList[currentPanelIndex].SetActive(false);
        
        if(currentPanelIndex == panelList.Count - 1)
            currentPanelIndex = 0;
        else 
            currentPanelIndex++;
        
        panelList[currentPanelIndex].SetActive(true);
    }

    public void SetPrevPanel()
    {
        Debug.Log("HidePrevPanel");
        panelList[currentPanelIndex].SetActive(false);
        
        if (currentPanelIndex == 0)
            currentPanelIndex = panelList.Count - 1;
        else
            currentPanelIndex--;
        
        panelList[currentPanelIndex].SetActive(true);
    }
}