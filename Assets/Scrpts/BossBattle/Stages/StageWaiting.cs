using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWaiting : Stage
{
    public float durationWaiting = 5;

    private void Start()
    {
        base.Start();
    }
    public override void OnStageStart()
    {
        zemar = FindObjectOfType<Zemar>();
        zemar.Move(zemar.defaultPosition.position, 0);
        Debug.Log(zemar.canMakeDamage);
        zemar.canMakeDamage = false;

        base.OnStageStart();
    }
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
