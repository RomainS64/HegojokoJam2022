using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [HideInInspector] public bool IsScrolling{get;set;}

    [SerializeField] private Transform[] leftPoints;
    [SerializeField] private Transform[] rightPoints;
    [SerializeField] private Transform[] skyPoints;
    private void Start()
    {
        IsScrolling = true;
    }
    public Transform[] getLeftSpawnPoint()
    {
        return leftPoints;
    }
    public Transform[] getRightSpawnPoint()
    {
        return rightPoints;
    }
    public Transform[] getSkySpawnPoint()
    {
        return skyPoints;
    }

}
