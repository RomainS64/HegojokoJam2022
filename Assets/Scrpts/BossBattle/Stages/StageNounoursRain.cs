using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageNounoursRain : Stage
{
    public Transform positionFlyingZemar;

    public GameObject enemyToSpawn;
    public Vector2Int mixMaxNumSpawns;

    public float speedZemarDeplacement = 2.5f;
    public float durationlaunchSpawnAfterZamarFlies = 4;
    public float durationBetweenSpawns;
    public float durationAfterLastNounoursSpawns = 4;

    public Transform[] pointSpawnNounours;
    private int numSpawns;
    private int oldSpawnPointIndexUsed = -1;

    private void Start()
    {
        base.Start();
    }
    public override void OnStageStart()
    {
        zemar.canMakeDamage = false;

        numSpawns = Random.Range(mixMaxNumSpawns.x, mixMaxNumSpawns.y + 1);

        base.OnStageStart();
    }

    public override void MakeActions()
    {
        zemar.Move(positionFlyingZemar.position, speedZemarDeplacement);

        StartCoroutine(WaitBeforeLaunchingNounoursSpawns());
    }

    private IEnumerator WaitBeforeLaunchingNounoursSpawns()
    {
        //Animations... tout le bordel.
        yield return new WaitForSeconds(durationlaunchSpawnAfterZamarFlies);
        StartCoroutine(SpawnFallingNounours());
    }

    private IEnumerator SpawnFallingNounours()
    {
        for (int i = 0; i < numSpawns; i++)
        {
            int randomSpotToSpawnIndex;
            do
            {
                randomSpotToSpawnIndex = Random.Range(0, pointSpawnNounours.Length - 1);
            } while (randomSpotToSpawnIndex == oldSpawnPointIndexUsed);

            oldSpawnPointIndexUsed = randomSpotToSpawnIndex;
            
            Transform randomSpotToSpawn = pointSpawnNounours[Random.Range(0, pointSpawnNounours.Length - 1)];
            Instantiate(enemyToSpawn, randomSpotToSpawn.position, randomSpotToSpawn.rotation);

            //Attendre le temps qu'il faut entre chaque spawn de monstre
            yield return new WaitForSeconds(durationBetweenSpawns);
        }

        StartCoroutine(WaitForNounoursToFall());
    }

    private IEnumerator WaitForNounoursToFall()
    {
        zemar.MoveToDefaultPosition(speedZemarDeplacement);

        yield return new WaitForSeconds(durationAfterLastNounoursSpawns);

        OnStageEnd();
    }
}
