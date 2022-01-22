using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageNaruto : Stage
{
    public Transform pointPositionDedoublement;
    public Transform pointPositionDedoublementOutOfScreen;
    public float speedZemarNaruto = 3;

    public float durationBeforeDedoublement = 3;
    public float durationBeforeGoingUpWhileDedoubling = 1.5f;

    public ZemarClone[] zemarClones;

    public override void OnStageStart()
    {
        zemar.canMakeDamage = false;
        zemar.MoveToDefaultPosition();

        base.OnStageStart();
    }
    public override void MakeActions()
    {
        zemar.Move(pointPositionDedoublement.transform.position, speedZemarNaruto);
        StartCoroutine(TimerBeforeDedoublement());
    }

    private IEnumerator TimerBeforeDedoublement()
    {
        yield return new WaitForSeconds(durationBeforeDedoublement);

        zemar.TriggerDedoublementAnimation();
        StartCoroutine(TimerBeforeGoingUpWhileDedoubling());
    }

    private IEnumerator TimerBeforeGoingUpWhileDedoubling()
    {
        yield return new WaitForSeconds(durationBeforeGoingUpWhileDedoubling);
    }
}
