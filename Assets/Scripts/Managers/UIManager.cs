using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameStateChangedCallBack;
    }
    
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallBack;
    }

    private void Start()
    {
        progressBar.value = 0;
        gamePanel.SetActive(false);
        levelText.text = "Level: "+(ChunkManager.Instance.GetLevel()+1);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.Instance.IsGameState())
        {
          return;
        }
        
        float progress = PlayerController.Instance.transform.position.z /
            ChunkManager.Instance.GetFinishZPosition();
        progressBar.value = progress;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void ShowGameOverPanel()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    
    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if (gameState==GameManager.GameState.GameOver)
        {
            ShowGameOverPanel();
        }else if (gameState==GameManager.GameState.LevelComplete)
        {
            ShowLevelComplete();
        }
    }
    
    private void ShowLevelComplete()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }
    
}
