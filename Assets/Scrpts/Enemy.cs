using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health;

    [HideInInspector]
    public Transform player;

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
       // player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomSpriteColor();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            int randHealth = Random.Range(0, 101);

            /*if(randHealth <= healthPickupChance)
            {
                Instantiate(healthPickup, transform.position, transform.rotation);
            }*/

            Destroy(gameObject);
        }
    }

    public void SetRandomSpriteColor()
    {
        Color newColor = everyNounoursColors.allColors[Random.Range(0, everyNounoursColors.allColors.Length)];
        spriteRenderer.color = new Color(newColor.r, newColor.g, newColor.b, 255);
    }
}
