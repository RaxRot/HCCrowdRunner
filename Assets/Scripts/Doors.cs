using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BonusType
{
    Addition,
    Difference,
    Product,
    Division
}

public class Doors : MonoBehaviour
{
   
    [Header("Elements")]
    [SerializeField] private SpriteRenderer rightDoorRenderer;
    [SerializeField] private TMP_Text rightDoorText;
    [SerializeField] private SpriteRenderer leftDoorRenderer;
    [SerializeField] private TMP_Text leftDoorText;
    [SerializeField] private Collider doorsCollider;

    [Header("Settings")] 
    [SerializeField] private BonusType rightDoorBonusType;
    [SerializeField] private int rightDoorBonusAmount;
    [SerializeField] private BonusType leftDoorBonusType;
    [SerializeField] private int leftDoorBonusAmount;
    [SerializeField] private Color bonusColor;
    [SerializeField] private Color penaltyColor;

    private void Start()
    {
        ConfigureDoors();
    }

    private void ConfigureDoors()
    {
        ConfigureRightDoor();

        ConfigureLeftDoor();
    }

    private void ConfigureLeftDoor()
    {
        switch (leftDoorBonusType)
        {
            case BonusType.Addition:
                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "+" + leftDoorBonusAmount;
                break;

            case BonusType.Difference:
                leftDoorRenderer.color = penaltyColor;
                leftDoorText.text = "-" + leftDoorBonusAmount;
                break;

            case BonusType.Product:
                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "x" + leftDoorBonusAmount;
                break;

            case BonusType.Division:
                leftDoorRenderer.color = penaltyColor;
                leftDoorText.text = "/" + leftDoorBonusAmount;
                break;

            default:
                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "+" + leftDoorBonusAmount;
                break;
        }
    }

    private void ConfigureRightDoor()
    {
        switch (rightDoorBonusType)
        {
            case BonusType.Addition:
                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "+" + rightDoorBonusAmount;
                break;

            case BonusType.Difference:
                rightDoorRenderer.color = penaltyColor;
                rightDoorText.text = "-" + rightDoorBonusAmount;
                break;

            case BonusType.Product:
                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "x" + rightDoorBonusAmount;
                break;

            case BonusType.Division:
                rightDoorRenderer.color = penaltyColor;
                rightDoorText.text = "/" + rightDoorBonusAmount;
                break;

            default:
                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "+" + rightDoorBonusAmount;
                break;
        }
    }

    public int GetBonusAmount(float positionX)
    {
        if (positionX>0)
        {
            return rightDoorBonusAmount;
        }
        else
        {
            return leftDoorBonusAmount;
        }
    }

    public BonusType GetBonusType(float positionX)
    {
        if (positionX>0)
        {
            return rightDoorBonusType;
        }
        else
        {
            return leftDoorBonusType;
        }
    }

    public void Disable()
    {
        doorsCollider.enabled = false;
    }
}
