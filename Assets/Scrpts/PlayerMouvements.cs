using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvements : MonoBehaviour
{
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;
    [SerializeField] private Transform topY, downY;
    [SerializeField] private GameObject feet;

    private bool canPlayPasSound = true;
    private Camera camera;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private SpriteRenderer spriteFeet;
    private bool hitInvulnerable = false;
    private Vector2  projection= Vector2.zero;

    private int damageTakenByZemar = 5;
    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        camera = FindObjectOfType<Camera>();
        sprite = GetComponent<SpriteRenderer>();
        animator = feet.GetComponent<Animator>();
        spriteFeet = feet.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Zemar")
        {
            FindObjectOfType<Health>().DamagePlayer(damageTakenByZemar);
            GetHit(collision.transform.position);
        }
    }
    public void GetHit(Vector3 position)
    {
        FindObjectOfType<ScreenShake>().Shake(0.3f, 1f);
        if (hitInvulnerable) return;
        hitInvulnerable = true;
        FindObjectOfType<PlayerMouvements>().GetHit(transform.parent.position);
        Debug.Log("Yo");
        if (position.x > transform.position.x)
        {
            projection = new Vector2(-8f,0f);
        }
        else
        {
            projection = new Vector2(8f,0);
        }

        Invoke(nameof(EndHit), 0.25f);
    }
    private void EndHit()
    {
        hitInvulnerable = false;
        projection = Vector2.zero;
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
        rb.velocity = newVelocity+projection;
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            if (canPlayPasSound)
            {
                canPlayPasSound = false;
                AkSoundEngine.PostEvent("playWalk", gameObject);
                Invoke(nameof(ResetCanPasSound), 0.5f);
            }
           
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
    private void ResetCanPasSound()
    {
        canPlayPasSound = true;
    }

    public void SetCurrentDamageWhenHitByZemar(int pDamages)
    {
        damageTakenByZemar = pDamages;
    }
}
