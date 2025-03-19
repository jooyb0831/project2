using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Image panel;
    void OnEnable()
    {
        panel.DOFade(1.0f, 1.5f).OnComplete(() =>
        {
            
        });
    }

    void OnDisable()
    {
        panel.color = new Color(0, 0, 0, 215);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
