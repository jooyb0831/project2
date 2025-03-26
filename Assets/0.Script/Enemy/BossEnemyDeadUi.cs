using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossEnemyDeadUi : MonoBehaviour
{
    private Player p;
    private Vector3 camPos;
    private Vector3 targetPos;
    private bool statusCheck;
    [SerializeField] GameObject deadObj;
    [SerializeField] Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.state.Equals(Enemy.State.Dead))
        {
            float dist = Vector3.Distance(transform.position, p.transform.position);
            if (dist < 2f)
            {
                deadObj.SetActive(true);
                camPos = Camera.main.transform.position;
                targetPos = new Vector3(camPos.x, transform.position.y, camPos.z);
                transform.LookAt(targetPos);
            }
            else
            {
                deadObj.SetActive(false);
            }

        }

    }
}
