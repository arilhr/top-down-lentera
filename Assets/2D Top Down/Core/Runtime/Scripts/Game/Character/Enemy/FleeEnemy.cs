using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownLentera
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FleeEnemy : Enemy
    {
        #region Variables

        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private float _detectionArea;
        [SerializeField]
        private Vector2 _detectionOffset;
        [SerializeField]
        private LayerMask _target;

        private Rigidbody2D _rigidbody;
        private bool _isMoving = false;
        private Vector3 _moveDirection;

        #endregion

        protected override void Start()
        {
            base.Start();
            
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (isDead) return;
            
            DetectTarget();
        }

        private void FixedUpdate()
        {
            if (isDead) return;

            Move();
        }

        private void Move()
        {
            if (!_isMoving) return;

            _rigidbody.MovePosition(transform.position + _moveDirection * _moveSpeed * Time.fixedDeltaTime / 10f);
        }


        private void DetectTarget()
        {
            Vector2 point = new Vector2(transform.position.x, transform.position.y) + _detectionOffset;
            Collider2D collider = Physics2D.OverlapCircle(point, _detectionArea, _target);
            if (collider != null)
            {
                Vector3 targetPos = collider.transform.position;
                _moveDirection = transform.position - targetPos;
                _isMoving = true;
                return;
            }

            _isMoving = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector2 point = new Vector2(transform.position.x, transform.position.y) + _detectionOffset;
            Gizmos.DrawWireSphere(point, _detectionArea);
        }
    }
}
