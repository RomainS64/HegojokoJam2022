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

    public override void OnStageStart()
    {
        zemar.canMakeDamage = false;
        zemar.OnBulletHitsEvent += OnBulletHitsZemar_StageNaruto;
        zemar.ToggleLevitateAnimation(false);

        base.OnStageStart();
    }
    public override void OnStageEnd()
    {
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
        StartCoroutine(TimeBeforeSpawnClones());
    }

    private IEnumerator TimeBeforeSpawnClones()
    {
        yield return new WaitForSeconds(durationBeforeSpawnClones);
        SpawnZemarAndNarutoClonesAtRandomPoint();
        zemar.SetIsAmongClones(true);
        StartCoroutine(TimerBeforeClonesMakeDamage());
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
        yield return new WaitForSeconds(durationBeforeEndOfStage);
        OnStageEnd();
    }

    private IEnumerator TimerBeforeResetZemarPosition()
    {
        yield return new WaitForSeconds(durationBeforeZemarReappear);
        zemar.MoveToDefaultPosition();
        yield return new WaitForSeconds(durationBeforeEndOfStage);
        OnStageEnd();
    }

    private void SpawnZemarAndNarutoClonesAtRandomPoint()
    {
        List<Transform> transformList = new List<Transform>();

        //Random transform
        foreach (var zemarCloneTransf in zemarCloneTransforms)
        {
            transformList.Add(zemarCloneTransf.transform);
        }

        //SET ZEMAR POSITION
        int randomZemarPositionIndex = UnityEngine.Random.Range(0, transformList.Count);

        zemar.transform.position = transformList[randomZemarPositionIndex].position;
        zemar.Move(zemarCloneEndPosition[randomZemarPositionIndex].position, speedZemarNaruto);

        transformList.RemoveAt(randomZemarPositionIndex);

        //SET CLONE POSITION
        for (int i = 0; i < transformList.Count; i++)
        {
            GameObject zemarCloneInstance = Instantiate(zemarClonesPrefab[i]);

            int randomClonePositionIndex = UnityEngine.Random.Range(0, transformList.Count);

            zemarCloneInstance.transform.position = transformList[randomZemarPositionIndex].position;
            zemarCloneInstance.GetComponent<ZemarClone>().Move(zemarCloneEndPosition[randomZemarPositionIndex].position, speedZemarNaruto);
            zemarCloneInstance.GetComponent<ZemarClone>().OnBulletHitsEvent += OnBulletHitsClone_StageNaruto;

            transformList.RemoveAt(randomZemarPositionIndex);
        }
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
        StopCoroutine(TimerBeforeClonesMakeDamage());
        StartCoroutine(DestroyAllClones());
        zemar.SetIsAmongClones(false);

        zemar.canMakeDamage = true;
        zemar.MoveAndTraversePlayer(traversingMultiplier, speedZemarTraversingPlayer);

        StartCoroutine(TimerBeforeMakeZemarAppearFromRight());
    }

    private void BulletHasHitZemar()
    {
        StopCoroutine(TimerBeforeClonesMakeDamage());
        StartCoroutine(DestroyAllClones());
        zemar.SetIsAmongClones(false);

        zemar.MoveToDefaultPosition();
    }

    // --------------------------- EVENTS ------------------------
    private void OnBulletHitsClone_StageNaruto(object sender, EventArgs e)
    {
        BulletHasHitClone();
    }

    private void OnBulletHitsZemar_StageNaruto(object sender, EventArgs e)
    {
        BulletHasHitZemar();
    }
}