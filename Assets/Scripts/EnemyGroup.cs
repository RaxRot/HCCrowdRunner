using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Enemy enemyPrefab;

    [Header("Settings")] 
    [SerializeField] private int enemyAmount;
    [SerializeField] private float angle;
    [SerializeField] private float radius;

    private void Start()
    {
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            Vector3 enemyLocalPosition = GetRunnerLocalPosition(i);
            Vector3 enemyWorldPos = transform.TransformPoint(enemyLocalPosition);
            Instantiate(enemyPrefab, enemyWorldPos, Quaternion.identity,transform);
        }
    }
    
    private Vector3 GetRunnerLocalPosition(int runnerIndex)
    {
        float x = radius * Mathf.Sqrt(runnerIndex) * Mathf.Cos(Mathf.Deg2Rad*runnerIndex*angle);
        float z = radius * Mathf.Sqrt(runnerIndex) * Mathf.Sin(Mathf.Deg2Rad*runnerIndex*angle);

        Vector3 runnerLocalPos = new Vector3(x, 0, z);
        
        return runnerLocalPos;
    }
}
