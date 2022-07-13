using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownLentera
{
    public class Enemy : Character
    {
        #region Variables

        protected GameManager _gameManager;
        
        protected Vector3 _startPosition;

        #endregion
        
        protected virtual void Start()
        {
            _gameManager = GameManager.Instance;
            _startPosition = transform.position;
        }

        public override void Die()
        {
            base.Die();
            gameObject.SetActive(false);
            _gameManager.StartCoroutine(OnRespawn());
        }

        public override void Respawn()
        {
            base.Respawn();
            
            transform.position = _startPosition;
            gameObject.SetActive(true);
        }
    }
}
