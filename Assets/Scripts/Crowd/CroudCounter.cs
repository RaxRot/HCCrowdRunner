using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CroudCounter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TMP_Text crowdCounterText;
    [SerializeField] private Transform runnersParent;

    private void Update()
    {
        crowdCounterText.text = runnersParent.childCount.ToString();
    }
}
