using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownLentera
{
    public class Enemy : Character
    {
        public GameObject graphic;

        public override void Die()
        {
            base.Die();
            graphic.SetActive(false);
            StartCoroutine(OnRespawn());
        }

        public override void Respawn()
        {
            base.Respawn();
            graphic.SetActive(true);
        }
    }
}
