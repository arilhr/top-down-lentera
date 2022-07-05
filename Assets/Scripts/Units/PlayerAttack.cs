using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ranged Properties")]
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootSpeed = 10f;
    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private GameObject rangedWeapon;

    [Header("Melee Properties")]
    [SerializeField] private float attackArea = 1f;
    [SerializeField] private float timeBetweenMeleeAttack = 1f;
    [SerializeField] private Transform attackPos;
    [SerializeField] private GameObject meleeWeapon;

    [SerializeField] private LayerMask enemyLayer;

    // 0: Ranged, 1: Melee
    private int currentAttackType = 0;

    private Player player;

    private void Start()
    {
        UpdateAttackWeapon();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (GameManager.Instance.state != GameState.Start) return;
        
        ChangeAttackTypeInput();
        AttackInput();
    }

    private void AttackInput()
    {
        if (player.isDead) return;
        if (Input.GetMouseButtonDown(0))
        {
            switch (currentAttackType)
            {
                case 0:
                    RangedAttack();
                    break;
                case 1:
                    MeleeAttack();
                    break;
            }
        }
    }

    private void UpdateAttackWeapon()
    {

        switch (currentAttackType)
        {
            case 0:
                rangedWeapon.SetActive(true);
                meleeWeapon.SetActive(false);
                break;
            case 1:
                rangedWeapon.SetActive(false);
                meleeWeapon.SetActive(true);
                break;
        }
    }

    private void ChangeAttackTypeInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            currentAttackType = currentAttackType == 0 ? 1 : 0;
            UpdateAttackWeapon();
        }
    }

    private void RangedAttack()
    {
        Debug.Log($"ranged attack..");
        
        // instantiate bullet
        GameObject bulletObject = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        var bulletComp = bulletObject.GetComponent<Bullet>();

        // shoot bullet
        bulletComp.Shoot(shootSpeed, 3f, "Enemy");
    }

    private void MeleeAttack()
    {
        Debug.Log($"melee attack..");

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackArea, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackArea);
    }
}
