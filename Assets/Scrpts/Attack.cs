using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool hasAttack = false;
    private float cooldown = 4f;
    private int damages = 3;
    Enemy enemy;
    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !hasAttack && !enemy.isDead)
        {
         
            hasAttack = true;
            
            enemy.Attack();
            FindObjectOfType<Health>().DamagePlayer(damages);
            FindObjectOfType<PlayerMouvements>().GetHit(transform.parent.position);
            Invoke(nameof(ResetAttack), cooldown);
        }
    }
    private void ResetAttack()
    {
        hasAttack = false;
    }
}
