using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool hasAttack = false;
    private float cooldown = 4f;
    private int damages = 3;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !hasAttack)
        {
            Debug.Log("Jattack");
            hasAttack = true;
            GetComponentInParent<Enemy>().Attack();
            FindObjectOfType<Health>().DamagePlayer(damages);
            Invoke(nameof(ResetAttack), cooldown);
           
        }
    }
    private void ResetAttack()
    {
        hasAttack = false;
    }
}
