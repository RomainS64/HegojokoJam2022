using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageNaruto : Stage
{
    public Transform pointPositionDedoublement;
    public Transform pointPositionDedoublementOutOfScreen;
    public float speedZemarNaruto = 3;
    public float speedZemarTraversingPlayer = 10;
    public float speedZemarClones = 6;
    public float traversingMultiplier = 20;

    public float durationBeforeDedoublement = 3;
    public float durationBeforeSpawnClones = 6f;
    public float durationBeforeClonesMakeDamage = 4;
    public float durationBeforeDestroyingClones = 2;
    public float durationBeforeZemarReappear = 1;
    public float durationBeforeEndOfStage = 1;

    public Transform[] zemarCloneTransforms;
    public Transform[] zemarCloneEndPosition;
    public GameObject[] zemarClonesPrefab;

    private IEnumerator timerBeforeClonesMakeDamage;
    private int numCloneOnScreen;
    public override void OnStageStart()
    {
        zemar.canMakeDamage = false;
        zemar.OnBulletHitsEvent += OnBulletHitsZemar_StageNaruto;
        zemar.OnEndNarutoAnimation += OnEndNarutoAnimation_StageNaruto;
        zemar.ToggleLevitateAnimation(false);

        base.OnStageStart();
    }
    public override void OnStageEnd()
    {
        numCloneOnScreen = 0;
        zemar.canMakeDamage = false;
        zemar.ToggleLevitateAnimation(true);

        base.OnStageEnd();
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
        //StartCoroutine(TimeBeforeSpawnClones());
    }

    private IEnumerator TimeBeforeSpawnClones()
    {
        Debug.Log("proutcacabitecouille");
        yield return new WaitForSeconds(0);
    }

    private IEnumerator TimerBeforeClonesMakeDamage()
    {
        yield return new WaitForSeconds(durationBeforeSpawnClones);
        BulletHasHitClone();
    }

    private IEnumerator TimerBeforeMakeZemarAppearFromRight()
    {
        yield return new WaitForSeconds(durationBeforeZemarReappear);
        zemar.AppearFromRightSide();
        zemar.ToggleLevitateAnimation(true);

        yield return new WaitForSeconds(durationBeforeEndOfStage);
        OnStageEnd();
    }

    private IEnumerator TimerBeforeResetZemarPosition()
    {
        yield return new WaitForSeconds(durationBeforeZemarReappear);
        zemar.MoveToDefaultPosition();
        zemar.ToggleLevitateAnimation(true);

        yield return new WaitForSeconds(durationBeforeEndOfStage);
        OnStageEnd();
    }

    private void SpawnZemarAndNarutoClonesAtRandomPoint()
    {
        Debug.Log(numCloneOnScreen);
        if (numCloneOnScreen >= 5) return;

        List<int> indexAlreadyUsed = new List<int>();

        //SET ZEMAR POSITION
        int randomZemarPositionIndex = UnityEngine.Random.Range(0, zemarCloneTransforms.Length);

        zemar.transform.position = zemarCloneTransforms[randomZemarPositionIndex].position;
        zemar.Move(zemarCloneEndPosition[randomZemarPositionIndex].position, speedZemarClones);

        indexAlreadyUsed.Add(randomZemarPositionIndex);
        //transformList.RemoveAt(randomZemarPositionIndex);

        //SET CLONE POSITION
        for (int i = 0; i < zemarCloneTransforms.Length - 1; i++)
        {
            GameObject zemarCloneInstance = Instantiate(zemarClonesPrefab[i]);
            int randomClonePositionIndex;
            bool isAlreadyUsed = false;

            do
            {
                isAlreadyUsed = false;

                randomClonePositionIndex = UnityEngine.Random.Range(0, zemarCloneTransforms.Length);

                foreach (int index in indexAlreadyUsed)
                {
                    if (index == randomClonePositionIndex)
                    {
                        isAlreadyUsed = true;
                    }
                }
            } while (isAlreadyUsed);

            zemarCloneInstance.transform.position = zemarCloneTransforms[randomClonePositionIndex].position;
            zemarCloneInstance.GetComponent<ZemarClone>().Move(zemarCloneEndPosition[randomClonePositionIndex].position, speedZemarClones);
            zemarCloneInstance.GetComponent<ZemarClone>().OnBulletHitsEvent += OnBulletHitsClone_StageNaruto;

            //transformList.RemoveAt(randomClonePositionIndex);
            indexAlreadyUsed.Add(randomClonePositionIndex);

            numCloneOnScreen++;
        }
        Debug.Log("Wtf");
    }

    private IEnumerator DestroyAllClones()
    {
        ZemarClone[] allClones = FindObjectsOfType<ZemarClone>();

        foreach (var clone in allClones)
        {
            clone.TriggerPoufAnimation();
        }

        yield return new WaitForSeconds(durationBeforeDestroyingClones);

        foreach (var clone in allClones)
        {
            Destroy(clone.gameObject);
        }
    }
    private void BulletHasHitClone()
    {
        StopCoroutine(timerBeforeClonesMakeDamage);
        StartCoroutine(DestroyAllClones());
        zemar.SetIsAmongClones(false);

        zemar.canMakeDamage = true;
        zemar.MoveAndTraversePlayer(traversingMultiplier, speedZemarTraversingPlayer);

        StartCoroutine(TimerBeforeMakeZemarAppearFromRight());
    }

    private void BulletHasHitZemar()
    {
        StopCoroutine(timerBeforeClonesMakeDamage);
        StartCoroutine(DestroyAllClones());
        zemar.SetIsAmongClones(false);

        StartCoroutine(TimerBeforeResetZemarPosition());
    }

    // --------------------------- EVENTS ------------------------
    private void OnEndNarutoAnimation_StageNaruto(object sender, EventArgs e)
    {//
        SpawnZemarAndNarutoClonesAtRandomPoint();
        zemar.SetIsAmongClones(true);

        timerBeforeClonesMakeDamage = TimerBeforeClonesMakeDamage();
        StartCoroutine(timerBeforeClonesMakeDamage);
    }
    private void OnBulletHitsClone_StageNaruto(object sender, EventArgs e)
    {
        BulletHasHitClone();
    }

    private void OnBulletHitsZemar_StageNaruto(object sender, EventArgs e)
    {
        BulletHasHitZemar();
    }
}