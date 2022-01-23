using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZemarClone : MonoBehaviour
{
    private PlayerMouvements player;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private Vector2 pointToMoveOn;
    private float zemarSpeed;

    public event EventHandler OnBulletHitsEvent;

    public GameObject bubullePrefab;

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
        Debug.Log("WHALALA2");
        animator.SetTrigger("Pouf");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GameObject bubulle = Instantiate(bubullePrefab);
            bubulle.transform.position = transform.position + new Vector3(0, 0, -1);

            Destroy(collision.gameObject);
            if (OnBulletHitsEvent != null) OnBulletHitsEvent(this, EventArgs.Empty);
        }
    }
}
