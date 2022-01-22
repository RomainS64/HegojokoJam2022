using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float curHealth;
    public float maxHealth;

    public Slider healthBar;
<<<<<<< HEAD
=======

    public float CurrentHealth
    {
        get { return curHealth; }
    }
>>>>>>> 72fc1059ec1a21b091f85c07e94f01f4cf32dc81

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
<<<<<<< HEAD
=======
    }
   
    //heal player
    public void HealPlayer (int healValue)
    {
        curHealth += healValue;

        healthBar.value = curHealth;
>>>>>>> 72fc1059ec1a21b091f85c07e94f01f4cf32dc81
    }
}
