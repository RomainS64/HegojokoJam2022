using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWaiting : Stage
{
    public float durationWaiting = 5;
    public override void MakeActions()
    {
        StartCoroutine(LaunchNewStageTimer());
    }

    private IEnumerator LaunchNewStageTimer()
    {
        yield return new WaitForSeconds(durationWaiting);
        OnStageEnd();
    }
}
