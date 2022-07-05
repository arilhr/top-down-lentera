using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public SpriteRenderer graphic;

    public override void Die()
    {
        base.Die();
        graphic.enabled = false;
        StartCoroutine(OnRespawn());
    }

    public override void Respawn()
    {
        base.Respawn();
        graphic.enabled = true;
    }
}
