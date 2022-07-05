using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Rigidbody2D rb;

    private Player player;

    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (GameManager.Instance.state != GameState.Start) return;

        LookToCursor();
        InputMove();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InputMove()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void Move()
    {
        if (player.isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        rb.velocity = moveInput * speed;
    }

    private void LookToCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        // search angle axis
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);

        // rotate player object
        transform.rotation = angleAxis;
    }

}
