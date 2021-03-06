using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Transform target;

    public string tagTarget = "Player";

    //TODO  : C'EST LE TRUC QUI DEFINI SI LE NOUNOURS VOLE OU PAS
    public bool isFlying = false;
    public bool isDead = false;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;

    public SpriteRenderer spriteRenderer,spriteDesYeux;
    public EveryNounoursColors everyNounoursColors;
    public Animator animator;
    public Collider2D colliderNounours;

    private IEnumerator followRoutine;

    public void Attack()
    {
        if (isDead) return;

        if(followRoutine != null)
        {
            StopCoroutine(followRoutine);
        }

        if (!isFlying)
        {
            animator.SetTrigger("Attack");
            Invoke(nameof(StartRoutine), 4f);
        }
    }
    private void StartRoutine()
    {
        if (isDead) return;
        followRoutine = FollowTarget();
        StartCoroutine(followRoutine);
    }
    //Elles sont publiques car elles ont besoin d'être utilisée dans d'autres scrits/sous-classes

    public virtual void Start()
    {

       target = GameObject.FindGameObjectWithTag(tagTarget).transform;
       SetRandomSpriteColor();
        if(!isFlying)
        {
            StartRoutine();
        }

    }
    IEnumerator FollowTarget()
    {
        while (true)
        {
            Vector3 dir = (target.position- transform.position).normalized;
            if (dir.x > 0)
            {
                spriteRenderer.flipX = true;
                spriteDesYeux.flipX = true;
            }
            else if (dir.x < 0)
            {
                spriteDesYeux.flipX = false;
                spriteRenderer.flipX = false;
            }
            transform.Translate(dir * speed/500);
            yield return new WaitForFixedUpdate();
        }
    }
    public void Kill()
    {
        FindObjectOfType<ScreenShake>().Shake(0.2f, 0.5f);
        EndPreparation.AddKill(transform.position, isFlying);

        if(isFlying)
        {
            GetComponent<ParachuteMovement>().isSleeping = true;
        }

        isDead = true;
        AkSoundEngine.PostEvent("playBisounoursDeath", gameObject);
        animator.SetTrigger("Sleep");
        colliderNounours.gameObject.tag = "NounoursMort";
        colliderNounours.enabled = false;
        if(followRoutine != null)StopCoroutine(followRoutine);
        Invoke(nameof(SetStatic), 2f);
    }
    private void SetStatic()
    {
        gameObject.isStatic = true;
    }
    public void SetRandomSpriteColor()
    {
        Color newColor = everyNounoursColors.allColors[Random.Range(0, everyNounoursColors.allColors.Length)];
        spriteRenderer.color = new Color(newColor.r, newColor.g, newColor.b, 255);
    }
}
