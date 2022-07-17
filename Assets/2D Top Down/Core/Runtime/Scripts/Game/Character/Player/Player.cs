using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownLentera
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : Character
    {
        #region Variables

        [SerializeField] 
        private float speed = 10f;

        [SerializeField] 
        private GameObject _graphics;
        [SerializeField] 
        private Rigidbody2D _rigidbody;

        private Character _lastAttacker;
        private GameManager _gameManager;
        private Vector2 _moveInput = Vector2.zero;
        private bool _flipped = false;
        private Vector3 _startPosition;

        #endregion

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _startPosition = transform.position;
        }

        private void Update()
        {
            if (_gameManager.State != GameState.Start) return;

            CheckFlip();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (isDead || _gameManager.State != GameState.Start)
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }

            _rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
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

        public override void TakeDamage(int damage, Character damager = null)
        {
            _lastAttacker = damager;
            
            base.TakeDamage(damage, damager);

            Debug.Log($"Player has been attacked by {damager.name}");
        }

        public override void Die()
        {
            base.Die();

            gameObject.SetActive(false);
            CameraController.Instance.SetTarget(_lastAttacker.transform);
            _gameManager.StartCoroutine(OnRespawn());
        }

        public override void Respawn()
        {
            base.Respawn();

            gameObject.SetActive(true);
            transform.position = _startPosition;

            CameraController.Instance.SetTarget(transform);
        }
    }
}
