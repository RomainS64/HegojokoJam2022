using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvements : MonoBehaviour
{
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;
    [SerializeField] private Transform topY, downY;
    [SerializeField] private GameObject feet;

    private Camera camera;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private SpriteRenderer spriteFeet;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camera = FindObjectOfType<Camera>();
        sprite = GetComponent<SpriteRenderer>();
        animator = feet.GetComponent<Animator>();
        spriteFeet = feet.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {


        Vector2 Point_1 = camera.ScreenToWorldPoint(Input.mousePosition);

        if (Point_1.x > transform.position.x)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
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
        Vector2 newVelocity = new Vector2(horizontal*speedX,vertical*speedY);
        rb.velocity = newVelocity;
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            AkSoundEngine.PostEvent("playWalk", gameObject);
            animator.SetBool("isMoving", true);
            if (rb.velocity.x > 0)
            {
                spriteFeet.flipX = false;
            }
            else
            {
                spriteFeet.flipX = true;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
