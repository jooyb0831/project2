using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Player player;
    private PlayerData pd;
    private Inventory inven;

    [SerializeField] GameObject UI;

    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        inven = GameManager.Instance.Inven;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist < 2f)
        {
            UI.SetActive(true);
            Vector3 pos = new Vector3(player.transform.position.x, UI.transform.position.y, player.transform.position.z);
            UI.transform.LookAt(pos);

            if(Input.GetKeyDown(KeyCode.F))
            {
                GameManager.Instance.isPaused = true;
                Camera.main.GetComponent<CameraMove>().enabled = false;
                CookingUI.Instance.EnableWindow();
            }
        }
        else
        {
            UI.SetActive(false);
        }
    }
}
