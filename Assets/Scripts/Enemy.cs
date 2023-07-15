using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State
    {
        Idle,
        Running
    }

    [Header("Settings")] 
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private State _state;
    private Transform _targetRunner;

    private void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch (_state)
        {
            case State.Idle:
                SearchForTargets();
                break;
            
            case State.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void RunTowardsTarget()
    {
        if (_targetRunner==null)
        {
            return;
        }

        transform.position =
            Vector3.MoveTowards(transform.position, _targetRunner.position, 
                moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position,_targetRunner.position)<=0.25f)
        {
            Destroy(_targetRunner.gameObject);
            Destroy(gameObject);
        }
    }

    private void SearchForTargets()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                /*if (runner.IsTarget())
                {
                    continue;
                }
                */

                runner.SetTarget();
                _targetRunner = runner.transform;
                
                StartRunningTowardsTarget();
            }
        }
    }

    private void StartRunningTowardsTarget()
    {
        _state = State.Running;
        GetComponent<Animator>().Play(TagManager.PLAYER_RUN_ANIM_NAME);
    }
}
