using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossBattle : MonoBehaviour
{
    public Stage[] stages;

    private Stage currentStage;
    private int currentStageIndex;

    private bool isInWalkingStage;
    // Start is called before the first frame update
    void Start()
    {
        currentStageIndex = 0;
        currentStage = stages[currentStageIndex];

        currentStage.OnStageStart();

        currentStage.OnStageEndingEvent += BossBattle_OnStageEnd;
    }

    private void BossBattle_OnStageEnd(object sender, EventArgs e)
    {
        Debug.Log("Start next stage");
        StartNextStage();
    }
    private void StartNextStage()
    {
        currentStage.OnStageEndingEvent -= BossBattle_OnStageEnd;

        currentStageIndex = (currentStageIndex + 1) % stages.Length;
        currentStage = stages[currentStageIndex];
        currentStage.OnStageStart();

        currentStage.OnStageEndingEvent += BossBattle_OnStageEnd;
    }
}
