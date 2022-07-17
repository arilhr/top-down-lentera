using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownLentera
{
    public class RangedWeapon : Weapon
    {
        #region Variables

        [SerializeField] 
        private Projectile _projectile;
        [SerializeField] 
        private int _ammo = 5;
        [SerializeField] 
        private int _damage = 1;
        [SerializeField] 
        private float _force = 10f;
        [SerializeField] 
        private float _delayShoot = 1f;

        private ObjectPooler _projectilePooler;
        private bool _canShoot = true;

        #endregion

        #region Methods

        private void Start()
        {
            _projectilePooler = ObjectPooler.Create(_projectile.gameObject);
            _projectilePooler.Init(_ammo);
        }

        private void OnEnable()
        {
            _canShoot = true;
        }
        
        public override void Attack(Character owner, string target, Vector2 direction)
        {
            if (_ammo == 0) return;
            if (!_canShoot) return;

            base.Attack(owner, target, direction);

            // spawn projetile
            GameObject projectileObj = _projectilePooler.SpawnObject(transform.position, Quaternion.identity);

            // find shoot direction and shoot projectile
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            projectile.Shoot(owner, target, direction, _force, _damage);

            StartCoroutine(DelayShoot());
        }

        private IEnumerator DelayShoot()
        {
            _canShoot = false;

            yield return new WaitForSeconds(_delayShoot);

            _canShoot = true;
        }

        #endregion
    }
}
