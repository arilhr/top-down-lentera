using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private int _ammo = 5;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _force = 10f;

    private ObjectPooler _projectilePooler;
    private Vector2 _direction = Vector2.zero;

    private void Start()
    {
        _projectilePooler = ObjectPooler.Create(_projectile.gameObject);
        _projectilePooler.Init(_ammo);
    }

    private void Update()
    {
        LookToCursor();
    }

    public override void Attack(Character owner, string target)
    {
        if (_ammo == 0) return;

        // spawn projetile
        GameObject projectileObj = _projectilePooler.SpawnObject(transform.position, Quaternion.identity);

        // find shoot direction and shoot projectile
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.Shoot(owner, target, _direction, _force, _damage);
    }

    private void LookToCursor()
    {
        _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);

        // rotate player object
        transform.rotation = angleAxis;
    }
}
