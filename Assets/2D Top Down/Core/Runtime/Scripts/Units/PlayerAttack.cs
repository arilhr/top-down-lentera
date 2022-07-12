using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeReference] private List<Weapon> weapons;

    [SerializeField] private LayerMask enemyLayer;

    // 0: Ranged, 1: Melee
    private int currentWeapon = 0;

    private Player _player;
    private GameManager _gameManager;

    private void Start()
    {
        _player = GetComponent<Player>();
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (_gameManager.state != GameState.Start) return;
        
        AttackInput();
    }

    private void AttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapons[currentWeapon].Attack(_player, "Enemy");
        }

        if (Input.GetMouseButtonDown(1))
        {
            ChangeAttackTypeInput();
        }
    }

    private void ChangeAttackTypeInput()
    {
        if (currentWeapon == weapons.Count - 1)
        {
            currentWeapon = 0;
            return;
        }

        currentWeapon++;
    }

}
