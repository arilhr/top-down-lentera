using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    public static Player Instance;

    [SerializeField] private GameObject graphics;
    
    private Character lastAttacker;

    [Header("UI")]
    public TMP_Text healthUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (healthUI != null)
        {
            healthUI.text = "HP: " + health.ToString();
        }
    }

    public void SetLastAttacker(Character _lastAttacker)
    {
        lastAttacker = _lastAttacker;
    }

    public override void Die()
    {
        base.Die();

        graphics.SetActive(false);
        CameraController.Instance.SetTarget(lastAttacker.transform);
        StartCoroutine(OnRespawn());
    }

    public override void Respawn()
    {
        base.Respawn();

        graphics.SetActive(true);
        CameraController.Instance.SetTarget(transform);
    }
}
