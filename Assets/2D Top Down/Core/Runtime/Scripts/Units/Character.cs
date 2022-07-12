using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    [Header("Stats")]
    public int health = 3;
    public float respawnTime = 3f;
    public bool isDead = false;

    public virtual void TakeDamage(int damage)
    {
        Debug.Log($"{name} took {damage} damage");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Respawn()
    {
        isDead = false;
        health = 3;
    }

    public virtual void Die()
    {
        isDead = true;
    }

    public IEnumerator OnRespawn()
    {
        yield return new WaitForSeconds(respawnTime);

        Respawn();
    }
}