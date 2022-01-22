using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Zemar : MonoBehaviour
{
    public Transform defaultPosition;
    public PlayerMouvements player;

    public const float speedAppearFromRightSide = 1.2f;
    public Transform positionToAppearFromRightSide;

    public SpriteRenderer spriteRenderer;

    public float currentDamageToPlayer = 10;
    public bool isInvincible = false;
    public bool canMakeDamage = false;

    private Animator animator;
    private Vector2 pointToMoveOn;
    private float zemarSpeed;

    public event EventHandler OnBulletHitsEvent;
    private bool isAmongClones;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMouvements>();

        pointToMoveOn = transform.position;
        zemarSpeed = 0;
        transform.position = defaultPosition.position;

        ToggleLevitateAnimation(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, pointToMoveOn, zemarSpeed * Time.deltaTime);

        if(player.transform.position.x < transform.position.x)
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

    public void MoveAndTraversePlayer(float traversingMultiplier, float speed)
    {
        pointToMoveOn = player.transform.position * traversingMultiplier;
        zemarSpeed = speed;
    }
    public void MoveToDefaultPosition(float speed = speedAppearFromRightSide)
    {
        pointToMoveOn = defaultPosition.position;
        zemarSpeed = speed;
    }

    public void AppearFromRightSide(float speed = speedAppearFromRightSide)
    {
        transform.position = positionToAppearFromRightSide.transform.position;
        MoveToDefaultPosition(speed);
    }

    public void SetSpriteLookRight(bool lookRight)
    {
        spriteRenderer.flipX = lookRight;
    }

    public void ToggleLevitateAnimation(bool isLevitating)
    {
        animator.SetBool("isLevitating", isLevitating);
    }

    public void TriggerDedoublementAnimation()
    {
        animator.SetTrigger("Dedoublement");
    }

    public void SetIsAmongClones(bool pIsAmongClones)
    {
        isAmongClones = pIsAmongClones;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            if(isAmongClones)
            {
                isAmongClones = false;
                if (OnBulletHitsEvent != null) OnBulletHitsEvent(this, EventArgs.Empty);
            }
            else
            {
                Debug.Log("Oulala je prend des degats");
            }
        }
    }
}
