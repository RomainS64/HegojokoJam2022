using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private float moveTime;
    [SerializeField] private float stopTime;
    [SerializeField] private float frogSpeed;
    [SerializeField] private float pistonSpeed;

    private GreMove[] grenouilles;
    private PistonMove piston;
    private AudioSource pistonSound;

    private void Start()
    {
        piston = FindObjectOfType<PistonMove>();
        grenouilles = FindObjectsOfType<GreMove>();
        pistonSound = GetComponent<AudioSource>();

        foreach (GreMove gre in grenouilles)
        {
            gre.moveTime = moveTime;
            gre.stopTime = stopTime;
            gre.speed = frogSpeed;
        }
        piston.speed = pistonSpeed;
        piston.downTime = stopTime / 4;
        piston.stopTime = stopTime / 4;
        piston.upTime = stopTime / 4;
        piston.endTime = stopTime / 4;

        StartCoroutine(nameof(Move));
    }
    public void StopMove()
    {
        StopAllCoroutines();
    }
    IEnumerator Move()
    {
        
        while (true)
        {
            bool allReady = true;
            bool allReadyToBeAtomized = true;
            foreach (GreMove gre in grenouilles)
            {
                if (!gre.isReady)
                {
                    allReady = false;
                   
                }
                if (!gre.isReadyToBeAtomized)
                {
                    allReadyToBeAtomized = false;
                }
            }

            if (allReady)
            {
                foreach (GreMove gre in grenouilles) gre.StartGreMove();
            }
            if (allReadyToBeAtomized)
            {
                foreach (GreMove gre in grenouilles) gre.isReadyToBeAtomized = false;
                pistonSound.Play();
                piston.StartPiston();
            }
            yield return null;
        }
        

    }
}
