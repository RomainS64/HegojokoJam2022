using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject fadeCanvas;
    [SerializeField] private GameObject introContent;
    [SerializeField] private GameObject InGameCanvas;
    [SerializeField] private GameObject scroller;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform pointCinematic, pointInGame;
    private WaveSpawner waveSpawner;
    float cinematiqueTime = 17.5f;
    void Start()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
        StartCoroutine(IntroCinematique());
    }
    IEnumerator IntroCinematique()
    {
        introContent.SetActive(true);
        InGameCanvas.SetActive(false);
        player.SetActive(false);
        scroller.transform.position = pointCinematic.position;
        yield return new WaitForSeconds(cinematiqueTime);
        fadeCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        player.SetActive(true);
        introContent.SetActive(false);
        scroller.transform.position = pointInGame.position;
        yield return new WaitForSeconds(2f);
        fadeCanvas.SetActive(false);
        InGameCanvas.SetActive(true);
        waveSpawner.StartFirstWave();
        AkSoundEngine.PostEvent("playMusicGog", gameObject);
    }
}
