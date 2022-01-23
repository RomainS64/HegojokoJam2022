using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float curHealth;
    public float maxHealth;

    public Slider healthBar;

    public GameObject endCanvas,uiCanvas,fadeSprite;
    public GameObject sleepingPlayerPrefab;
    private PlayerMouvements player;
    private bool isDead = false;
    public float CurrentHealth
    {
        get { return curHealth; }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMouvements>();
        curHealth = maxHealth;
        healthBar.value = curHealth;
        //healthBar.maxValue = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            DamaadagePlayer(10);
        }*/
    }

    public void DamagePlayer( int damageValue )
    {
        curHealth -= damageValue;

        healthBar.value = curHealth;
        if(curHealth <= 0 && !isDead)
        {
            isDead = true;
            gameObject.SetActive(false);
            player.GetComponent<SpriteRenderer>().enabled = false;
            uiCanvas.SetActive(false);
            endCanvas.SetActive(true);
            fadeSprite.SetActive(true);
            fadeSprite.GetComponentInParent<Scroller>().StopScrolling();
            GameObject sleepingPlayer = Instantiate(sleepingPlayerPrefab);
            sleepingPlayer.transform.position = new Vector3(
                player.transform.position.x,
                player.transform.position.y,
                -9);

        }
        else
        {
            AkSoundEngine.PostEvent("playDmg", gameObject);
        }

    }
   
    //heal player
    public void HealPlayer (float healValue)
    {
        curHealth += healValue;

        healthBar.value = curHealth;

    }
}
