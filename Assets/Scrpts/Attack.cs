using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool hasAttack = false;
    public float cooldown = 4f;
    public int damages = 5;
    public Enemy enemy;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !hasAttack)
        {
         
            hasAttack = true;

            if (enemy != null)
            {
                if (enemy.isDead) return;
                enemy.Attack();
            }
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
