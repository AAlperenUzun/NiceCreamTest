using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    [HideInInspector]
    public bool isAvailable = true;

    private bool isMaking = false;

    public Product productPrefab;
    
    
    public string standName = "DefaultProduct";
    public float basePrice = 10.0f;
    public float baseCreationTime = 5.0f;
    public StandType _standType;
    public float currentCreationTime { get; private set; }
    public float currentPrice{ get; private set; }

    public Transform upgradeUi;
    

    public void UpdateValues()
    {
        var time= UpgradeSystem.Instance.GetValue(GeneralUpgradeType.AllStandSpeedUp, 1);
        time*= UpgradeSystem.Instance.GetValue(UpgradeType.StandSpeedUp, _standType, 1);
        currentCreationTime = baseCreationTime/time;
        // Debug.LogError("Creation time is:" + currentCreationTime);
        var price= UpgradeSystem.Instance.GetValue(GeneralUpgradeType.AllMultiplier, 1);
        price*= UpgradeSystem.Instance.GetValue(UpgradeType.StandMultiplier, _standType, 1);
        currentPrice = basePrice * price;
        // Debug.LogError("Price is:" + currentPrice);
    }
    public Product Made()
    {
        isAvailable = true;
        isMaking = false;
        UpdateValues();
        var tempProduct = Instantiate(productPrefab);
        tempProduct.SetValues(standName, currentPrice);
        return tempProduct;
    }

    public void SetUpgradeUI(bool isActive)
    {
        upgradeUi.gameObject.SetActive(isActive);
    }
}
