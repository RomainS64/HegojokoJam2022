using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyAndSpawnParameters
    {
        public Enemy typeOfEnemy;
        public int numberOfEnemies;
    }

    [System.Serializable]
    public class Wave
    {
        public EnemyAndSpawnParameters[] enemiesAndSpawnParameters;
        public Vector2 minMaxTimeBetweenSpawns;
        public float timeBeforeStartingWave;
        public bool isScrolling;
    }

    public int waveIndexToStart = 0;

    public Text waveInformationText;
    public GameObject gameManager;

    public Wave[] waves;
    public Transform[] spawnPointsRight;
    public Transform[] spawnPointsLeft;
    public Transform[] spawnPointsUp;

    private Wave currentWave;
    private int currentWaveIndex;//INDEX = INDICE
    private Transform player;

    private bool finishedSpawning;

    private void Start()
    {
        //AudioManager.instance.PlaySound("musicGame");

        //player = GameObject.FindGameObjectWithTag("Player").transform;
        currentWaveIndex = waveIndexToStart;
        waveInformationText.text = "Wave n°" + (currentWaveIndex+1);
    }

    public void StartFirstWave()
    {
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    private IEnumerator StartNextWave(int index)
    {//On attend timeBetweenWaves secondes, puis on lance une vague
        currentWave = waves[index];//Le vague courante est celle désignée par currentWaveIndex

        yield return new WaitForSeconds(currentWave.timeBeforeStartingWave);
        StartCoroutine(SpawnWave(index));
    }

    private IEnumerator SpawnWave(int index)
    {
        //Récupérer tous les ennmis dans une liste.
        List<Enemy> enemies = GetAllEnemiesOfCurrentWave();
        int totalOfEnemiesOfCurrentWave = enemies.Count;

        for (int i = 0; i < totalOfEnemiesOfCurrentWave ; i++)
        {
            if(player == null)
            {//S'il est mort, on arrête de spawn de monstres
                yield break;
            }
            
            Enemy randomEnemy = enemies[UnityEngine.Random.Range(0, enemies.Count)];
            enemies.RemoveAt(i);

            Transform randomSpotToSpawn = GetRandomSpawnPoint(enemies[UnityEngine.Random.Range(0, enemies.Count)]);
            Instantiate(randomEnemy, randomSpotToSpawn.position, randomSpotToSpawn.rotation);

            //Détection de la fin de la vague
            if(i == totalOfEnemiesOfCurrentWave - 1)
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }

            //Attendre le temps qu'il faut entre chaque spawn de monstre
            yield return new WaitForSeconds(UnityEngine.Random.Range(currentWave.minMaxTimeBetweenSpawns.x, currentWave.minMaxTimeBetweenSpawns.y));
        }
    }

    private void Update()
    {
        if (finishedSpawning == true && GameObject.FindGameObjectsWithTag("Nounours").Length == 0)
        {
            finishedSpawning = false;//Si on a finit la vague, on setup la suivante
            if(currentWaveIndex + 1 < waves.Length)//S'il y a encore une vague/des vagues
            {
                PrepareNextWave();
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else//S'il n'y en a plus
            {
                Debug.Log("gg la street t'as gagné");
            }
        }
    }

    private void PrepareNextWave()
    {
        currentWaveIndex++;
        waveInformationText.text = "Wave n°" + (currentWaveIndex + 1);
        Debug.Log("La vague " + (currentWaveIndex + 1) + " va commencer...");
    }

    private Transform GetRandomSpawnPoint(Enemy enemyToSpawn)
    {
        if (enemyToSpawn.isFlying)
        {//FLYING ENEMIES ARE SURE FLYING SO THEY SPAWN IN THE SKY
            return spawnPointsUp[UnityEngine.Random.Range(0, spawnPointsUp.Length)];
        }

        if (currentWave.isScrolling)
        {//SPAWN RIGHT BECAUSE ENEMIES CANT SPAWN ON THE RIGHT
            return spawnPointsRight[UnityEngine.Random.Range(0, spawnPointsRight.Length)];
        }
        else
        {
            int randomSide = UnityEngine.Random.Range(1, 3);
            if(randomSide == 1)
            {//SPAWN RIGHT
                return spawnPointsRight[UnityEngine.Random.Range(0, spawnPointsRight.Length)];
            }
            else
            {//SPAWN LEFT
                return spawnPointsLeft[UnityEngine.Random.Range(0, spawnPointsLeft.Length)];
            }
        }
    }

    private List<Enemy> GetAllEnemiesOfCurrentWave()
    {
        List<Enemy> enemies = new List<Enemy>();

        foreach (var enemiesAndSpawnParameters in currentWave.enemiesAndSpawnParameters)
        {
            for (int i = 0; i < enemiesAndSpawnParameters.numberOfEnemies; i++)
            {
                enemies.Add(enemiesAndSpawnParameters.typeOfEnemy);
            }
        }

        return enemies;
    }
}
