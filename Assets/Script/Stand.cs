using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    [HideInInspector]
    public bool isAvailable = true;

    private bool isMaking = false;

    private Cashier currentCashier;
    public Product productPrefab;
    private UpgradeManager _upgradeManager;
    
    
    public string standName = "DefaultProduct";
    public float basePrice = 10.0f;
    [HideInInspector]
    public float baseCreationTime = 5.0f;
    [HideInInspector]
    private float tempCurrentTime;
    public StandType _standType;
    private float currentCreationTime;
    private float currentPrice;

    private void Start()
    {
        productPrefab.currentPrice = basePrice;
    }

    private void SetStand()
    {
        
    }

    public void Init(UpgradeManager upgradeManager)
    {
        _upgradeManager = upgradeManager;
    }

    public void StartMake(Cashier cashier)
    {
        currentCashier = cashier;
        tempCurrentTime = 0;
        isMaking = true;
        
        var time= UpgradeSystem.Instance.GetValue(GeneralUpgradeType.AllStandSpeedUp, 1);
         time*= UpgradeSystem.Instance.GetValue(UpgradeType.StandSpeedUp, _standType, 1);
         currentCreationTime = baseCreationTime/time;
    }

    private void Making()
    {
        tempCurrentTime += Time.deltaTime;
      
        if (tempCurrentTime>currentCreationTime)
        {
            Made();
        }
    }

    private void Made()
    {
        isAvailable = true;
        isMaking = false;
        var tempProduct = Instantiate(productPrefab, currentCashier.handT);
        var price= UpgradeSystem.Instance.GetValue(GeneralUpgradeType.AllMultiplier, 1);
        price*= UpgradeSystem.Instance.GetValue(UpgradeType.StandMultiplier, _standType, 1);
        currentPrice = basePrice * price;
        tempProduct.SetValues(standName, currentPrice);
        currentCashier.TakeFood(tempProduct);
        currentCashier = null;
    }

    private void Update()
    {
        if (isMaking)
        {
            Making();
        }
    }
}
