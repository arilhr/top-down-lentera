using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownLentera
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected float timeToDestroy = 3f;
        [SerializeField] private Rigidbody2D _rigidbody;
        private Character _owner;
        private string _target;
        private int _damage;

        public void Shoot(Character owner, string target, Vector2 direction, float force, int damage)
        {
            _owner = owner;
            _target = target;
            _damage = damage;

            // set rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = angleAxis;

            _rigidbody.AddForce(transform.right * force, ForceMode2D.Impulse);

            StartCoroutine(WaitToDestroy());
        }

        protected IEnumerator WaitToDestroy()
        {
            yield return new WaitForSeconds(timeToDestroy);

            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_target))
            {
                Character character = collision.GetComponent<Character>();

                if (character.isDead) return;

                if (character != null) character.TakeDamage(_damage, _owner);
            }

            if (collision.gameObject == _owner.gameObject) return;
        
            gameObject.SetActive(false);
        }
    }
}
