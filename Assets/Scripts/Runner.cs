using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [Header("Settings")] 
    private bool _isTarget;
    
    public bool IsTarget()
    {
        return _isTarget;
    }

    public void SetTarget()
    {
        _isTarget = true;
    }
    
    
}
