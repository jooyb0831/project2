using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : Tool
{
    private GameSystem gameSystem;

    [SerializeField] ParticleSystem woodParticle;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        data.toolNmae = "도끼";
        data.useST = 3;
        data.lv = 1;
        gameSystem = GameManager.Instance.GameSystem;
    }

    public override void SetTool()
    {
        base.SetTool();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Tree>() && p.state.Equals(Player.State.Mine))
        {
            woodParticle.Play();
        }
    }
}
