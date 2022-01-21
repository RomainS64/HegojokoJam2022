using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        /*Dans le tableau d'ennemies, on a
         * col1 : le type de l'ennemie présent dans le tableau
         *col 2 : le nombre d'ennemie
        */
        public Enemy[] enemies;//Contient tous les enemies qui spawneront dans la wave
        public float timeBetweenSpawns;
    }

    public Animator canvasAnimator;

    public int waveIndexToStart = 0;

    public Text waveInformationText;
    public GameObject gameManager;

    public Wave[] waves;
    public Transform[] spawnPoints;//Tableau contenant tous les points de spawn possible
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;//INDEX = INDICE
    private Transform player;

    private bool finishedSpawning;


    private void Start()
    {
        //AudioManager.instance.PlaySound("musicGame");

        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentWaveIndex = waveIndexToStart;

        waveInformationText.text = "Wave n°" + (currentWaveIndex+1);
        

        //Commencer la première vague de monstres
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {//On attend timeBetweenWaves secondes, puis on lance une vague
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];//Le vague courante est celle désignée par currentWaveIndex

        for(int i = 0; i < currentWave.enemies.Length ; i++)
        {
            if(player == null)
            {//S'il est mort, on arrête de spawn de monstres
                yield break;
            }

            //On sélectionne un ennemie aléatoirement dans le tableau wave puis on le place aléatoirement dans la map

            /*Gestion du nombre d'ennemies dans une wave
             S'il*/
 
            //currentEnemy -> l'ennemi courant lors du parcours du tableau
            Enemy currentEnemy = currentWave.enemies[i];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(currentEnemy, randomSpot.position, randomSpot.rotation);

            //Détection de la fin de la vague
            if(i == currentWave.enemies.Length - 1)
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }
            //Attendre le temps qu'il faut entre chaque spawn de monstre
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    private void Update()
    {
        if (finishedSpawning == true && GameObject.FindGameObjectsWithTag("Mob").Length == 0)
        {
            finishedSpawning = false;//Si on a finit la vague, on setup la suivante
            if(currentWaveIndex + 1 < waves.Length)//S'il y a encore une vague/des vagues
            {
                currentWaveIndex++;
                waveInformationText.text = "Wave n°" + (currentWaveIndex+1);
                canvasAnimator.SetTrigger("TextBlop");
                Debug.Log("La vague " + (currentWaveIndex + 1) + " va commencer...");
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else//S'il n'y en a plus
            {
                GameObject vieJoueur = GameObject.FindGameObjectWithTag("Player");
                GameObject vieTour = GameObject.FindGameObjectWithTag("Tower");
                //if (vieJoueur.GetComponent<HeartSystem>().life > 0 && vieTour.GetComponent<HeartSystem>().life > 0)
                //{
                //    gameManager.GetComponent<scr_SceneManager>().LoadWin();
                //}
            }
        }
    }
}
