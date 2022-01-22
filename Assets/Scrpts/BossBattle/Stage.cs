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

    public StateStage currentState;
    public event EventHandler OnStageEndingEvent;
    public void OnStageStart()
    {
        currentState = StateStage.STARTING;
        Debug.Log("On start");
        currentState = StateStage.RUNNING;
    }

    public void OnStageRunning()
    {
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
        OnStageEnd();
    }
}
