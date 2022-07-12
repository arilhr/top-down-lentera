using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    public static Player Instance;

    [SerializeField] private float speed = 10f;

    [SerializeField] private GameObject _graphics;
    [SerializeField] private Rigidbody2D _rigidbody;

    private Character _lastAttacker;
    private GameManager _gameManager;
    private Vector2 _moveInput = Vector2.zero;
    private bool _flipped = false;
    

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (_gameManager.state != GameState.Start) return;

        CheckFlip();
        InputMove();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InputMove()
    {
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void Move()
    {
        if (isDead)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        _rigidbody.velocity = _moveInput * speed;
    }

    private void CheckFlip()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        if (direction.x > 0 && _flipped || direction.x < 0 && !_flipped)
        {
            var objScale = transform.localScale;
            objScale.x *= -1;
            transform.localScale = objScale;
            _flipped = !_flipped;
        }
    }

    public void SetLastAttacker(Character lastAttacker)
    {
        _lastAttacker = lastAttacker;
    }

    public override void Die()
    {
        base.Die();

        _graphics.SetActive(false);
        CameraController.Instance.SetTarget(_lastAttacker.transform);
        StartCoroutine(OnRespawn());
    }

    public override void Respawn()
    {
        base.Respawn();

        _graphics.SetActive(true);
        CameraController.Instance.SetTarget(transform);
    }
}