using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPreparation : MonoBehaviour
{
    static public void ResetPrefs()
    {
        
    }
    static public void AddKill(Vector3 pos,bool isPendu = false)
    {
        PlayerPrefs.SetInt("nbKill", PlayerPrefs.GetInt("nbKill", 0) + 1);
    }
}
