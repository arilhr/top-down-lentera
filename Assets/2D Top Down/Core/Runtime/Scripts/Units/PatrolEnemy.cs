using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownLentera
{
    public class PatrolEnemy : Enemy
    {
        [SerializeField] protected float _speed = 5f;
        [SerializeField] protected float _delayBetweenMove = 3f;
        [SerializeField] protected List<Vector3> _movePositions;
    
        protected bool _isMoving = true;
        protected int _currentTarget = 0;

        private void OnEnable()
        {
            _isMoving = true;
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            if (!_isMoving) return;
            if (isDead) return;

            if (Vector3.Distance(transform.position, _movePositions[_currentTarget]) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _movePositions[_currentTarget], _speed * Time.deltaTime / 10);
                return;
            }

            _currentTarget++;
            if (_currentTarget >= _movePositions.Count)
            {
                _currentTarget = 0;
            }

            _isMoving = false;
            StartCoroutine(WaitNextMove());
        }

        public IEnumerator WaitNextMove()
        {
            yield return new WaitForSeconds(_delayBetweenMove);

            _isMoving = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isDead) return;

            Player player = collision.gameObject.GetComponent<Player>();
            if (player == null) return;

            player.TakeDamage(1, this);
        }
    }
}
