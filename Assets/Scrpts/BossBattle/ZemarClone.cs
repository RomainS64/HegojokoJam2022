using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZemarClone : MonoBehaviour
{
    private PlayerMouvements player;
    public SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector2 pointToMoveOn;
    private float zemarSpeed;

    public event EventHandler OnBulletHitsEvent;

    void Start()
    {
        player = FindObjectOfType<PlayerMouvements>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, pointToMoveOn, zemarSpeed * Time.deltaTime);

        if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void Move(Vector2 endPoint, float speed)
    {
        pointToMoveOn = endPoint;
        zemarSpeed = speed;
    }

    public void TriggerPoufAnimation()
    {
        Debug.Log("Pouf");
        //animator.SetTrigger("Pouf");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if (OnBulletHitsEvent != null) OnBulletHitsEvent(this, EventArgs.Empty);
        }
    }
}
