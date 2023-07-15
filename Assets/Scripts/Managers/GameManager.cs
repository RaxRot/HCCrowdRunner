using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action<GameState> OnGameStateChanged;
    
    public enum GameState
    {
        Menu,
        Game,
        LevelComplete,
        GameOver
    }
    private GameState _gameState;

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

    public void SetGameState(GameState gameState)
    {
        _gameState = gameState;
        OnGameStateChanged?.Invoke(_gameState);
    }

    public bool IsGameState()
    {
        return _gameState == GameState.Game;
    }
}
