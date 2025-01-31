using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill0 : Skill
{
    
    [SerializeField] float delay;
    [SerializeField] float coolTimer;
    [SerializeField] float timer;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SetData(0);
    }
}
