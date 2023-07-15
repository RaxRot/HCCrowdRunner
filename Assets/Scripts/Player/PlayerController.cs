using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [Header("Elements")] 
    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private float roadWidth = 10;
    [SerializeField] private PlayerAnimator playerAnimator;
    
    [Header("Settings")] 
    [SerializeField] private float moveSpeed;
    private bool _canMove;

    [Header("Control")]
    [SerializeField] private float slideSpeed;
    private Vector3 _clickedScreenPosition;
    private Vector3 _clickedPlayerPosition;

    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallBack;
    }
    
    private void Update()
    {
        if (_canMove)
        {
            MoveForward();
            ManageControl();
        }
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime*moveSpeed;
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _clickedScreenPosition = Input.mousePosition;
            _clickedPlayerPosition = transform.position;
        }else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - _clickedScreenPosition.x;
            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            Vector3 position = transform.position;
            position.x = _clickedPlayerPosition.x + xScreenDifference;
            position.x = Mathf.Clamp(position.x,-roadWidth/2+crowdSystem.GetCrowdRadius(),
                roadWidth/2-crowdSystem.GetCrowdRadius());
            
            transform.position = position;
        }
    }

    private void StartMoving()
    {
        _canMove = true;
        playerAnimator.Run();
    }

    private void StopMoving()
    {
        _canMove = false;
        playerAnimator.Idle();
    }
    
    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if (gameState==GameManager.GameState.Game)
        {
            StartMoving();
        }
        else  if (gameState==GameManager.GameState.GameOver || gameState==GameManager.GameState.LevelComplete)
        {
            StopMoving();
        }
    }
}
