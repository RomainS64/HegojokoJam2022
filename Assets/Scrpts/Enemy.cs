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

    public SpriteRenderer spriteRenderer;
    public EveryNounoursColors everyNounoursColors;
    //Elles sont publiques car elles ont besoin d'être utilisée dans d'autres scrits/sous-classes

    public virtual void Start()
    {
       target = GameObject.FindGameObjectWithTag(tagTarget).transform;
       SetRandomSpriteColor();
       StartCoroutine(FollowTarget());
    }
    IEnumerator FollowTarget()
    {
        while (true)
        {
            Vector3 dir = (target.position- transform.position).normalized;
            if (dir.x>0) spriteRenderer.flipX = false;
            else if (dir.x<0) spriteRenderer.flipX = true;
            transform.Translate(dir * speed/500);
            yield return new WaitForFixedUpdate();
        }
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
    public void SetRandomSpriteColor()
    {
        Color newColor = everyNounoursColors.allColors[Random.Range(0, everyNounoursColors.allColors.Length)];
        spriteRenderer.color = new Color(newColor.r, newColor.g, newColor.b, 255);
    }
}
