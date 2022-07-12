using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownLentera
{
    public class ShootEnemy : PatrolEnemy
    {
        #region Variables

        // Random: shoot random direction
        // Nearby: shoot when near target
        [System.Serializable]
        enum ShootType
        {
            Random,
            Nearby
        }

        [Header("Shoot Properties")]
        [SerializeField]
        private ShootType _shootType = ShootType.Random;
        [SerializeField]
        private Weapon _weapon;
        [SerializeField]
        private LayerMask _target;

        [Header("Nearby Shoot Properties")]
        [SerializeField]
        private float _detectionAreaSize = 1f;
        [SerializeField]
        private Vector2 _detectionAreaOffset = Vector2.zero;

        #endregion

        private void Update()
        {
            if (isDead) return;

            if (_movePositions.Count > 0) Move();

            Shoot();
        }

        private void Shoot()
        {
            switch (_shootType)
            {
                case ShootType.Random:
                    ShootRandomly();
                    break;
                case ShootType.Nearby:
                    ShootNearby();
                    break;
            }
        }

        private void ShootNearby()
        {
            Vector2 point = new Vector2(transform.position.x, transform.position.y) + _detectionAreaOffset;
            Collider2D[] collides = Physics2D.OverlapCircleAll(point, _detectionAreaSize, _target);
            foreach (Collider2D target in collides)
            {
                Vector2 direction = target.transform.position - transform.position;
                _weapon.Attack(this, "Player", direction);
            }
        }

        private void ShootRandomly()
        {
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            Vector2 direction = new Vector2(randomX, randomY);

            _weapon.Attack(this, "Player", direction);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector2 point = new Vector2(transform.position.x, transform.position.y) + _detectionAreaOffset;
            Gizmos.DrawWireSphere(point, _detectionAreaSize);
        }
    }
}
