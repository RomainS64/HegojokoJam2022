using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvements : MonoBehaviour
{
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;
    [SerializeField] private Transform topY, downY;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if ((transform.position.y<=downY.position.y && vertical<0)||
            (transform.position.y>=topY.position.y && vertical>0))
        {
            vertical = 0;
        }

        Vector2 newVelocity = new Vector2(horizontal,vertical);
        rb.velocity = newVelocity;
    }
}
