using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMoveAround : Stage
{
    public Transform[] transformPointsRight;
    public Transform[] transformPointsLeft;

    public Vector2Int mixMaxNumDeplacement;

    public float speedZemarDeplacement;
    public float durationToNextDeplacement;

    private int numDeplacements;
    private bool isRightSide;

    public override void OnStageStart()
    {
        isRightSide = true;
        zemar.canMakeDamage = false;

        numDeplacements = Random.Range(mixMaxNumDeplacement.x, mixMaxNumDeplacement.y + 1);

        base.OnStageStart();
    }

    public override void MakeActions()
    {
        StartCoroutine(WaitForNextMoving());
    }

    private IEnumerator WaitForNextMoving()
    {
        while(numDeplacements > 0)
        {
            zemar.Move(GetRandomPosition(), speedZemarDeplacement);

            numDeplacements--;
            isRightSide = !isRightSide;
            zemar.SetSpriteLookRight(!isRightSide);

            yield return new WaitForSeconds(durationToNextDeplacement);
        }

        StartCoroutine(MoveBackToDefaultPosition());
    }

    private Vector3 GetRandomPosition()
    {
        if(isRightSide)
        {
            return transformPointsLeft[Random.Range(0, transformPointsLeft.Length)].position;
        }
        else
        {
            return transformPointsRight[Random.Range(0, transformPointsRight.Length)].position;
        }
    }

    private IEnumerator MoveBackToDefaultPosition()
    {
        zemar.MoveToDefaultPosition(speedZemarDeplacement);

        yield return new WaitForSeconds(durationToNextDeplacement);
        OnStageEnd();
    }

}
