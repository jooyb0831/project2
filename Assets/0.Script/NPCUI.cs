using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NPCUI : MonoBehaviour
{
    public TMP_Text stringArea;
    int idx = 0;
    public List<string> basicDialogue;
    public List<string> yesDialogue;
    public List<string> noDialogue;
    [SerializeField] List<string> currentDalogue;
    [SerializeField] GameObject answerWindow;
    [SerializeField] GameObject nextBtn;
    [SerializeField] bool isBasicDialogue = true;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
