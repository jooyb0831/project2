using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharUI : Singleton<CharUI>
{
    [SerializeField] TMP_Text lvTxt;
    [SerializeField] TMP_Text expTxt;
    [SerializeField] TMP_Text hpTxt;
    [SerializeField] TMP_Text stTxt;
    [SerializeField] TMP_Text spTxt;
    [SerializeField] TMP_Text mpTxt;
    [SerializeField] TMP_Text atkTxt;
    [SerializeField] TMP_Text speedTxt;

    private Toggle toggle;

    private PlayerData pd;


    public int Level
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            lvTxt.text = $"Level : {pd.Level}";
        }
    }

    public int EXP
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            expTxt.text = $"EXP : {pd.EXP}/{pd.MAXEXP}";
        }
    }

    public int MAXEXP
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            
        }
    }

    public int HP
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            hpTxt.text = $"HP : {pd.MAXHP}";
        }
    }

    public int ST
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            stTxt.text = $"ST : {pd.ST}";
        }
    }

    public int SP
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            spTxt.text = $"SP : {pd.MAXSP}";
        }
    }

    public int MP
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            mpTxt.text = $"MP : {pd.MAXMP}";
        }
    }

    public int ATK
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            atkTxt.text = $"ATK : {pd.BasicAtk}";
        }
    }

    public int Speed
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            speedTxt.text = $"Speed : {pd.Speed}";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
        lvTxt.text = $"Level : {pd.Level}";
        expTxt.text = $"EXP : {pd.EXP}/{pd.MAXEXP}";
        hpTxt.text = $"HP : {pd.MAXHP}";
        stTxt.text = $"ST : {pd.ST}";
        spTxt.text = $"SP : {pd.MAXSP}";
        mpTxt.text = $"MP : {pd.MAXMP}";
        atkTxt.text = $"ATK : {pd.BasicAtk}";
        speedTxt.text = $"Speed : {pd.Speed}";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
