using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDash : Stage
{
    public Transform[] positionDashesRight;
    public Transform[] positionDashesLeft;

    public Transform offScreenBeforeDashesPosition;

    public float speedZemarDash = 10f;
    public float speedZemarGoingOffScreen = 4f;
    public Vector2Int minMaxNumDashes;

    public float durationBetweenZemarDashes = 4;
    public float durationRedSquareBeforeDash = 1;
    public float durationBeforeZemarMakeDashes = 4;
    public float durationAfterZemarResetPosition = 3;

    public Animator squareAnimator;

    private int numDashes;
    private int oldDashIndex = -1;
    private bool isDashOnRight;

    private void Start()
    {
        base.Start();
    }
    public override void OnStageStart()
    {
        zemar.canMakeDamage = true;
        isDashOnRight = true;

        numDashes = Random.Range(minMaxNumDashes.x, minMaxNumDashes.y + 1);

        base.OnStageStart();
    }

    public override void MakeActions()
    {
        zemar.ToggleLevitateAnimation(false);
        zemar.Move(offScreenBeforeDashesPosition.position, speedZemarGoingOffScreen);
        StartCoroutine(WaitBeforeStartingDashes());
    }

    private IEnumerator WaitBeforeStartingDashes()
    {
        //Animations... tout le bordel.
        yield return new WaitForSeconds(durationBeforeZemarMakeDashes);
        zemar.isDashing = true;
        zemar.ToggleWalking(true);
        StartCoroutine(StartMultipleDashes());
    }

    private IEnumerator StartMultipleDashes()
    {
        for (int i = 0; i < numDashes; i++)
        {
            int randomSpotToSpawnIndex;
            //do
            //{
            //    randomSpotToSpawnIndex = Random.Range(0, positionDashesRight.Length - 1);
            //} while (randomSpotToSpawnIndex == oldDashIndex);

            //oldDashIndex = randomSpotToSpawnIndex;

            randomSpotToSpawnIndex = Random.Range(0, positionDashesRight.Length - 1);

            ActivateRedZone(randomSpotToSpawnIndex);

            yield return new WaitForSeconds(durationRedSquareBeforeDash);

            if (isDashOnRight)
            {//Dash from right to left
                zemar.transform.position = positionDashesRight[randomSpotToSpawnIndex].position;
                zemar.Move(positionDashesLeft[randomSpotToSpawnIndex].position, speedZemarDash);
            }
            else
            {//Dash from left to right
                zemar.transform.position = positionDashesLeft[randomSpotToSpawnIndex].position;
                zemar.Move(positionDashesRight[randomSpotToSpawnIndex].position, speedZemarDash);
            }

            isDashOnRight = !isDashOnRight;
            zemar.SetSpriteLookRight(!isDashOnRight);

            //Attendre le temps qu'il faut entre chaque spawn de monstre
            yield return new WaitForSeconds(durationBetweenZemarDashes);
        }

        StartCoroutine(WaitForZemarToResetPosition());
    }

    private IEnumerator WaitForZemarToResetPosition()
    {
        zemar.ToggleWalking(false);
        zemar.ToggleLevitateAnimation(true);

        zemar.isDashing = false;
        zemar.AppearFromRightSide(speedZemarGoingOffScreen);

        yield return new WaitForSeconds(durationAfterZemarResetPosition);

        OnStageEnd();
    }

    private void ActivateRedZone(int indexOfZone)
    {
        switch(indexOfZone)
        {
            case 0:
                squareAnimator.SetTrigger("SquareUp");
                break;
            case 1:
                squareAnimator.SetTrigger("SquareMiddle");
                break;
            case 2:
                squareAnimator.SetTrigger("SquareDown");
                break;
            default:
                Debug.Log("IndexOfZone not working in ActiveRedZone...");
                break;
        }
    }
}
