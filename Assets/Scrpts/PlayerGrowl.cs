using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrowl : MonoBehaviour
{
    private float timer;
    private bool canPlaySound = true;

    // Update is called once per frame
    void Update()
    {
        timer = Random.Range(8, 20);
        if (canPlaySound)
        {
            canPlaySound = false;
            AkSoundEngine.PostEvent("playPunchline", gameObject);
            Invoke(nameof(StopTimer), timer);
        }
    }

    private void StopTimer()
    {
        canPlaySound = true;
    }
}
