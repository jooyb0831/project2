using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUIBtn : MonoBehaviour
{
    [SerializeField] GameObject quickQuestBG;
    [SerializeField] GameObject upIcon;
    [SerializeField] GameObject questIcon;
    bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            OnClickQUIBtn();
        }
    }
   
    public void OnClickQUIBtn()
    {
        
        if(isOn)
        {
            quickQuestBG.SetActive(false);
            upIcon.SetActive(false);
            questIcon.SetActive(true);
            isOn = false;
        }
        else
        {
            quickQuestBG.SetActive(true);
            questIcon.SetActive(false);
            upIcon.SetActive(true);
            isOn = true;
        }

    }
}
