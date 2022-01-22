using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stage : MonoBehaviour
{
    public enum StateStage
    {
        STARTING,
        RUNNING,
        ENDING
    }

    public string stageName = "No named stage";
    public StateStage currentState;
    public event EventHandler OnStageEndingEvent;
    public void OnStageStart()
    {
        currentState = StateStage.STARTING;
        Debug.Log("On start");
        OnStageRunning();
    }

    public void OnStageRunning()
    {
        currentState = StateStage.RUNNING;

        MakeActions();
    }

    public void OnStageEnd()
    {
        currentState = StateStage.ENDING;

        Debug.Log("On End");

        if (OnStageEndingEvent != null) OnStageEndingEvent(this, EventArgs.Empty);
    }

    public void MakeActions()
    {
        Debug.Log("Doing some action");
        StartCoroutine(LaunchNewStageTimer());
    }

    private IEnumerator LaunchNewStageTimer()
    {
        yield return new WaitForSeconds(2);
        OnStageEnd();

    }
}
