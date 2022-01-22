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
<<<<<<< HEAD
=======
>>>>>>> 20c8fd57a2c80f1ec6dff8f629b17a15c12843a8

    public float CurrentHealth
    {
        get { return curHealth; }
    }
<<<<<<< HEAD
=======
>>>>>>> 72fc1059ec1a21b091f85c07e94f01f4cf32dc81
>>>>>>> 20c8fd57a2c80f1ec6dff8f629b17a15c12843a8

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
<<<<<<< HEAD
=======
>>>>>>> 20c8fd57a2c80f1ec6dff8f629b17a15c12843a8
    }
   
    //heal player
    public void HealPlayer (int healValue)
    {
        curHealth += healValue;

        healthBar.value = curHealth;
<<<<<<< HEAD
=======
>>>>>>> 72fc1059ec1a21b091f85c07e94f01f4cf32dc81
>>>>>>> 20c8fd57a2c80f1ec6dff8f629b17a15c12843a8
    }
}
