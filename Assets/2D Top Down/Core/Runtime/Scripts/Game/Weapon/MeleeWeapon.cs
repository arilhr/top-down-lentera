using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownLentera
{
    public class MeleeWeapon : Weapon
    {
        #region Variables
        
        [SerializeField] private int _damage;
        [SerializeField] private Vector2 _areaSize;
        [SerializeField] private Vector2 _areaOffset;
        [SerializeField] private Animator _slashEffect;
        [SerializeField] private float _delayAttack = 1f;
        [SerializeField] private LayerMask _enemyLayer;

        private bool _canAttack = true;

        #endregion

        #region Mono Behaviours

        private void OnEnable()
        {
            _canAttack = true;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector2 point = new Vector2(transform.position.x, transform.position.y) + _areaOffset * transform.lossyScale;
            Gizmos.DrawWireCube(point, _areaSize);
        }

        #endregion

        #region Methods
        public override void Attack(Character owner, string target, Vector2 direction)
        {
            if (!_canAttack) return;

            base.Attack(owner, target, direction);
            
            _slashEffect.SetTrigger("Slash");

            Vector2 point = new Vector2(transform.position.x, transform.position.y) + _areaOffset * transform.lossyScale;
            Collider2D[] enemies = Physics2D.OverlapBoxAll(point, _areaSize, 0, _enemyLayer);
            foreach (Collider2D enemy in enemies)
            {
                Character characterDamaged = enemy.GetComponent<Character>();
                if (characterDamaged.isDead) continue;
                if (characterDamaged != null) characterDamaged.TakeDamage(1);
            }

            StartCoroutine(WaitDelayAttack());
        }

        private IEnumerator WaitDelayAttack()
        {
            _canAttack = false;

            yield return new WaitForSeconds(_delayAttack);

            _canAttack = true;
        }

        #endregion
    }

}
