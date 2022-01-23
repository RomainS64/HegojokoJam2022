using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    

    [SerializeField] private float scrollTime;
    [SerializeField] private GameObject deathBodyPrefab;
    [SerializeField] private Text T2, T3;
    Camera camera;
    IEnumerator scroller;

    private void Start()
    {
        camera = FindObjectOfType<Camera>();
        
        
        int nbKills = PlayerPrefs.GetInt("nbKill", 0);

        int coca = PlayerPrefs.GetInt("coca", 0);
        if (coca > 0)
        {
            T2.text = "You snorted " +coca+"00g of Big C.";
            T3.text = "You killed " + nbKills + " innocents.";
        }
        else
        {
            T2.text = "Hopefully, you didn't get addict to Big C.";
            T3.text = "However, you killed " + nbKills + " innocents.";
        }
        
        


        for (int i = 1; i <= nbKills; i++)
        {
            GameObject newBody = Instantiate(deathBodyPrefab);
            Debug.Log("Instantiate " + i + ":" + "(" + PlayerPrefs.GetFloat("posX" + i, 0) + "," + PlayerPrefs.GetFloat("posY" + i, 0));
            newBody.transform.position = new Vector3(
                PlayerPrefs.GetFloat("posX" +i,0),
                PlayerPrefs.GetFloat("posY" +i,0),
                0
                );
        }
        float scrollerPosX = PlayerPrefs.GetFloat("endX", 0);
        scroller = Scroller(new Vector3(scrollerPosX, 1, -10), new Vector3(0, 1, -10));
        StartCoroutine(scroller);

    }
    IEnumerator Scroller(Vector3 initPos,Vector3 endPos)
    {
        camera.transform.position = initPos;
        Vector3 dir = (initPos-endPos);
        for(int i = 0; i < 500; i++)
        {
            camera.transform.position -= dir / 500;
            yield return new WaitForSeconds(scrollTime / 500);
        }
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("Menu");

    }
}
