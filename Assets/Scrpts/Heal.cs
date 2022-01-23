using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{

    public float healValue;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EndPreparation.AddHeal();
            collision.gameObject.GetComponent<Health>().HealPlayer(healValue);
            Destroy(this.gameObject);
        }
    }
}
