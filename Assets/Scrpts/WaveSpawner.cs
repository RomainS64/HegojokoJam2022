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
        public GameObject enemy;
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

    private GameObject gameManager;

    public Wave[] waves;

    private Wave currentWave;
    private int currentWaveIndex;//INDEX = INDICE
    private Transform player;

    private Scroller scroller;

    private bool finishedSpawning;

    private void Start()
    {
        //gameManager = GameObject.FindObjectOfType(GameManager);
        currentWaveIndex = waveIndexToStart;

        scroller = FindObjectOfType<Scroller>();
    }

    public void StartFirstWave()
    {
        EndPreparation.ResetPrefs();
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    private IEnumerator StartNextWave(int index)
    {//On attend timeBetweenWaves secondes, puis on lance une vague
        currentWave = waves[index];//Le vague courante est celle désignée par currentWaveIndex
        UpdateScrolling();

        yield return new WaitForSeconds(currentWave.timeBeforeStartingWave);

        StartCoroutine(SpawnCurrentWave());
    }

    private IEnumerator SpawnCurrentWave()
    {
        //Récupérer tous les ennmis dans une liste.
        List<GameObject> enemies = GetAllEnemiesOfCurrentWave();
        int totalOfEnemiesOfCurrentWave = enemies.Count;

        for (int i = 0; i < totalOfEnemiesOfCurrentWave ; i++)
        {
            //if(player == null)
            //{//S'il est mort, on arrête de spawn de monstres
            //    yield break;
            //}

            int randomEnemyIndex = UnityEngine.Random.Range(0, enemies.Count);
            GameObject randomEnemy = enemies[randomEnemyIndex];
            enemies.RemoveAt(randomEnemyIndex);

            Transform randomSpotToSpawn = GetRandomSpawnPoint(randomEnemy.GetComponent<Enemy>());
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
                
                EndPreparation.SetEndX(scroller.transform.position.x);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Debug.Log("gg la street t'as gagné");
            }
        }
    }

    private void PrepareNextWave()
    {
        currentWaveIndex++;
        Debug.Log("La vague " + (currentWaveIndex + 1) + " va commencer...");
    }

    private void UpdateScrolling()
    {
        if (currentWave.isScrolling)
        {
            scroller.StartScrolling();
        }
        else
        {
            scroller.StopScrolling();
        }
    }

    private Transform GetRandomSpawnPoint(Enemy enemyToSpawn)
    {
        if (enemyToSpawn.isFlying)
        {//FLYING ENEMIES ARE SURE FLYING SO THEY SPAWN IN THE SKY
            return scroller.GetSkySpawnPoint()[UnityEngine.Random.Range(0, scroller.GetSkySpawnPoint().Length)];
        }

        if (currentWave.isScrolling)
        {//SPAWN RIGHT BECAUSE ENEMIES CANT SPAWN ON THE RIGHT
            return scroller.GetRightSpawnPoint()[UnityEngine.Random.Range(0, scroller.GetRightSpawnPoint().Length)];
        }
        else
        {
            int randomSide = UnityEngine.Random.Range(1, 3);
            if(randomSide == 1)
            {//SPAWN RIGHT
                return scroller.GetRightSpawnPoint()[UnityEngine.Random.Range(0, scroller.GetRightSpawnPoint().Length)];
            }
            else
            {//SPAWN LEFT
                return scroller.GetLeftSpawnPoint()[UnityEngine.Random.Range(0, scroller.GetLeftSpawnPoint().Length)];
            }
        }
    }

    private List<GameObject> GetAllEnemiesOfCurrentWave()
    {
        List<GameObject> enemies = new List<GameObject>();

        foreach (var enemiesAndSpawnParameters in currentWave.enemiesAndSpawnParameters)
        {
            for (int i = 0; i < enemiesAndSpawnParameters.numberOfEnemies; i++)
            {
                enemies.Add(enemiesAndSpawnParameters.enemy);
            }
        }

        return enemies;
    }
}
