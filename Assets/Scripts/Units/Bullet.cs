using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private string targetTag;

    public void Shoot(float force, float timeToDestroy, string target)
    {
        targetTag = target;
        rb.AddForce(transform.right * force, ForceMode2D.Impulse);
        StartCoroutine(DestroyBullet(timeToDestroy));
    }

    private IEnumerator DestroyBullet(float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            var character = collision.gameObject.GetComponent<Character>();
            character.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
