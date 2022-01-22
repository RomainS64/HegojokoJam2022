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
    [SerializeField] private GameObject go;
    IEnumerator ScrollRoutine;
    private void Start()
    {
        IsScrolling = false;
        StopGo();
    }
    public void StartScrolling()
    {
        if (ScrollRoutine != null) StopCoroutine(ScrollRoutine);
        ScrollRoutine = Scroll();
        go.SetActive(true);
        Invoke(nameof(StopGo), 2f);
        StartCoroutine(ScrollRoutine);
    }
    private void StopGo()
    {
        go.SetActive(false);
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
