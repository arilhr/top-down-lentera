using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject menuUI;
    public GameObject startUI;

    public TMP_Text dieText;
    public TMP_Text healthText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    public void ClickStart()
    {
        GameManager.Instance.StartGame();
    }

    private void Start()
    {
        GameManager.Instance.OnChangeState += (state) =>
        {
            menuUI.SetActive(state == GameState.Menu);
            startUI.SetActive(state == GameState.Start);
        };
    }
}
