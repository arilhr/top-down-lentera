using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownLentera
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Variables

        [SerializeReference] private List<Weapon> weapons;

        [SerializeField] private LayerMask enemyLayer;

        private int currentWeapon = 0;

        private Vector2 _direction = Vector2.zero;
        private Player _player;
        private GameManager _gameManager;

        #endregion

        #region Mono Behaviours

        private void Start()
        {
            _player = GetComponent<Player>();
            _gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (_gameManager.State != GameState.Start) return;

            AttackInput();
            LookToCursor();
        }

        #endregion

        #region Methods

        private void AttackInput()
        {
            if (_player.isDead) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                weapons[currentWeapon].Attack(_player, "Enemy", _direction);
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

        private void LookToCursor()
        {
            _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }

        #endregion
    }
}

