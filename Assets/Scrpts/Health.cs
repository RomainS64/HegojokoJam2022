using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float curHealth;
    public float maxHealth;

    public Slider healthBar;

    public float CurrentHealth
    {
        get { return curHealth; }
    }
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBar.value = curHealth;
        healthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DamagePlayer(10);
        }
    }

    public void DamagePlayer( int damageValue )
    {
        curHealth -= damageValue;

        healthBar.value = curHealth;

    }
   
    //heal player
    public void HealPlayer (int healValue)
    {
        curHealth += healValue;

        healthBar.value = curHealth;

    }
}
