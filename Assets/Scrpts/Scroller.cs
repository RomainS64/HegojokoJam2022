using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [HideInInspector] public bool IsScrolling{get;set;}
    [SerializeField] private Transform[] leftPoints;
    [SerializeField] private Transform[] rightPoints;
    [SerializeField] private Transform[] skyPoints;
    IEnumerator ScrollRoutine;
    private void Start()
    {
        StartScrolling();
        IsScrolling = true;
    }
    public void StartScrolling()
    {
        if (ScrollRoutine != null) StopCoroutine(ScrollRoutine);
        ScrollRoutine = Scroll();
        StartCoroutine(ScrollRoutine);
    }
    public void StopScrolling()
    {
        if (ScrollRoutine != null) StopCoroutine(ScrollRoutine);
    }
    public Transform[] GetLeftSpawnPoint()
    {
        return leftPoints;
    }
    public Transform[] GetRightSpawnPoint()
    {
        return rightPoints;
    }
    public Transform[] GetSkySpawnPoint()
    {
        return skyPoints;
    }

    IEnumerator Scroll()
    {
        while (true)
        {
            transform.position += new Vector3(scrollSpeed/100, 0, 0);
            yield return new WaitForFixedUpdate();
        }
        
    }
}
