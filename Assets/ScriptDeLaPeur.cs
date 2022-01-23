using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ScriptDeLaPeur : MonoBehaviour
{
    VideoPlayer video;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(LoadMenu), 11.00f);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }

  
}
