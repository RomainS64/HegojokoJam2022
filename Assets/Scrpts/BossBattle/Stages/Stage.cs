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

    private StateStage currentState;
    public event EventHandler OnStageEndingEvent;

    public int damageZemarWhileStage;

    protected Zemar zemar;


    protected virtual void Start()
    {
        zemar = FindObjectOfType<Zemar>();
    }

    public virtual void OnStageStart()
    {
        currentState = StateStage.STARTING;

        Debug.Log("On start : " + stageName);

        zemar.player.SetCurrentDamageWhenHitByZemar(damageZemarWhileStage);

        OnStageRunning();
    }

    public virtual void OnStageRunning()
    {
        currentState = StateStage.RUNNING;

        Debug.Log("On Running : " + stageName);

        MakeActions();
    }

    public virtual void OnStageEnd()
    {
        currentState = StateStage.ENDING;

        Debug.Log("On End : " + stageName);

        if (OnStageEndingEvent != null) OnStageEndingEvent(this, EventArgs.Empty);
    }

    public virtual void MakeActions()
    {
        StartCoroutine(LaunchNewStageTimer());
    }

    private IEnumerator LaunchNewStageTimer()
    {
        yield return new WaitForSeconds(1);
        OnStageEnd();
    }
}
