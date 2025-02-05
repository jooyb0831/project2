using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillClearWindow : MonoBehaviour
{
    [SerializeField] SkillEquipWindow skw;
    public bool isQslot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClearBtnClicked()
    {
        if(isQslot)
        {
            skw.ClearQSkill();
        }
        else
        {
            skw.ClearRSkill();
        }

        gameObject.SetActive(false);
    }
}
