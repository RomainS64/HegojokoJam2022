using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPreparation : MonoBehaviour
{
    static public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    static public void AddKill(Vector3 pos,bool isPendu = false)
    {
        Debug.Log("A:("+pos.x+","+pos.y+")");
        int id = PlayerPrefs.GetInt("nbKill", 0) + 1;
        PlayerPrefs.SetInt("nbKill",id);
        PlayerPrefs.SetFloat("posX" + id, pos.x);
        PlayerPrefs.SetFloat("posY" + id, pos.y);
        PlayerPrefs.SetInt("isPendu" + id, isPendu ? 1 : 0);
    }
    static public void AddHeal()
    {
        PlayerPrefs.SetInt("coca", PlayerPrefs.GetInt("coca",0)+1);
    }
    static public void SetEndX(float pos)
    {
        PlayerPrefs.SetFloat("endX", pos);
    }
}
