using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Enemy
{
    public float speed = 5f;
    public List<Vector3> movePos;
    public float timeBetweenMove = 3f;
    
    private bool isMoving = true;
    private int currentTarget = 0;

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (!isMoving) return;
        if (isDead) return;

        if (Vector3.Distance(transform.position, movePos[currentTarget]) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos[currentTarget], speed * Time.deltaTime);
        }
        else
        {
            currentTarget++;
            if (currentTarget >= movePos.Count)
            {
                currentTarget = 0;
            }

            isMoving = false;
            StartCoroutine(WaitNextMove());
        }
    }

    public IEnumerator WaitNextMove()
    {
        yield return new WaitForSeconds(timeBetweenMove);

        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        Player player = collision.gameObject.GetComponent<Player>();
        if (player == null) return;

        player.TakeDamage(1);
        player.SetLastAttacker(this);
    }
}
