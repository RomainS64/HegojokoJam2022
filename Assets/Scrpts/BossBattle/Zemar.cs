using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Zemar : MonoBehaviour
{
    public GameObject fade;
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
    public event EventHandler OnEndNarutoAnimation;

    public GameObject bubullePrefab;

    [SerializeField] private Slider zemarHealthBar;
    private bool isAmongClones;

    private float zemarCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMouvements>();

        transform.position = defaultPosition.position;
        pointToMoveOn = defaultPosition.position;
        zemarSpeed = 1;

        zemarCurrentHealth = 100;
        zemarHealthBar.value = zemarCurrentHealth;

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
        pointToMoveOn = new Vector2(player.transform.position.x * traversingMultiplier, player.transform.position.y * traversingMultiplier);
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
                TakeDamage(5.0f);
                if (zemarCurrentHealth <= 0)
                {
                    Death();
                }


                Destroy(collision.gameObject);
            }

            GameObject bubulle = Instantiate(bubullePrefab);
            bubulle.transform.position = transform.position + new Vector3(0, 0, -1);
        }
    }

    public void AnimatorKeyEvent_TriggerCloneSpawns()
    {
        if (OnEndNarutoAnimation != null) OnEndNarutoAnimation(this, EventArgs.Empty);
    }

    private void TakeDamage(float damage)
    {
        zemarCurrentHealth -= damage;
        zemarHealthBar.value = zemarCurrentHealth;
        AkSoundEngine.PostEvent("playZemmarDmg", gameObject);
    }

    public void ToggleWalking(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }

    public void ToggleLeveBras(bool isLevingBras)
    {
        animator.SetBool("isLevingBras", isLevingBras);
    }

    private void Death()
    {
        fade.SetActive(true);
        Invoke(nameof(EndGame),2f);
        Debug.Log("Oh non je suis mort et en fait je suis Zemmour");
    }
    private void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
