using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Transform runnersParent;
    [SerializeField] private GameObject runnerPrefab;
    [SerializeField] private PlayerAnimator playerAnimator;
    
    [Header("Settings")] 
    [SerializeField] private float angle;
    [SerializeField] private float radius;
    
    private void Update()
    {
       PlaceRunners();
       
       ControlPlayerHealth();
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 GetRunnerLocalPosition(int runnerIndex)
    {
        float x = radius * Mathf.Sqrt(runnerIndex) * Mathf.Cos(Mathf.Deg2Rad*runnerIndex*angle);
        float z = radius * Mathf.Sqrt(runnerIndex) * Mathf.Sin(Mathf.Deg2Rad*runnerIndex*angle);

        Vector3 runnerLocalPos = new Vector3(x, 0, z);
        
        return runnerLocalPos;
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch (bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;
            
            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                break;
            
            case BonusType.Product:
                int runnersToAdd = (runnersParent.childCount * bonusAmount)-runnersParent.childCount;
                AddRunners(runnersToAdd);
                break;
            
            case BonusType.Division:
                int runnersToRemove = runnersParent.childCount-(runnersParent.childCount/bonusAmount);
                RemoveRunners(runnersToRemove);
                break;
        }
    }

    private void AddRunners(int bonusAmount)
    {
        for (int i = 0; i < bonusAmount; i++)
        {
            Instantiate(runnerPrefab, runnersParent);
            
            playerAnimator.Run();
        }
    }

    private void RemoveRunners(int amount)
    {
        if (amount > runnersParent.childCount)
        {
            amount = runnersParent.childCount;
        }

        int runnersAmount = runnersParent.childCount;

        for (int i = runnersAmount-1; i >= runnersAmount-amount; i--)
        {
            Transform runnerToDestroy = runnersParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
        }
    }

    private void ControlPlayerHealth()
    {
        if (runnersParent.childCount<=0)
        {
            GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
        }
    }
}
