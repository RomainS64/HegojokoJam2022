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

    public float speed;
    public float timeBetweenAttacks;
    public int damage;

    public SpriteRenderer spriteRenderer,spriteDesYeux;
    public EveryNounoursColors everyNounoursColors;
    public Animator animator;

    private IEnumerator followRoutine;
    //Elles sont publiques car elles ont besoin d'être utilisée dans d'autres scrits/sous-classes

    public virtual void Start()
    {
       target = GameObject.FindGameObjectWithTag(tagTarget).transform;
       SetRandomSpriteColor();
       followRoutine = FollowTarget();
       StartCoroutine(followRoutine);
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
        animator.SetTrigger("Sleep");
        gameObject.tag = "NounoursMort";
        GetComponent<BoxCollider2D>().enabled = false;
        StopCoroutine(followRoutine);
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
