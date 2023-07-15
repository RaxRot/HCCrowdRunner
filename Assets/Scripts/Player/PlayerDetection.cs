using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private CrowdSystem crowdSystem;
    private bool _isFinished;
    
    private void Update()
    {
        DetectDoors();
    }

    private void DetectDoors()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1f);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            { 
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

               doors.Disable();

               crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }else if (detectedColliders[i].tag==TagManager.FINISH_LINE_TAG && !_isFinished)
            {
                _isFinished = true;
                
                PlayerPrefs.SetInt(TagManager.PREFS_LEVEL_KEY,PlayerPrefs.GetInt(TagManager.PREFS_LEVEL_KEY)+1);
                GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);
                
            }
            
        }
    }
}
