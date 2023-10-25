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
    public int baseUpgradePrice=9;
    public StandType _standType;
    public float currentCreationTime { get; private set; }
    public float currentPrice{ get; private set; }
    public float currentUpgradePrice{ get; private set; }
    public int currentLevel{ get; private set; }

    [SerializeField] private StandUpgrade upgradeUi;
    private int price;
    private int currentPriceRate=1;


    private void Awake()
    {
        currentLevel = 1;
        currentUpgradePrice = baseUpgradePrice;
        //6  7  8  9 10 11 12 13 14 x 31 34 37 40 43 46 50
        //9 10 12 14 17 21 25 30 35 x 42 51 61 73 87 104 125
        //+1  +2 +2 +3 +4 +4 +5 +5  +7 +9 +10+12+14+17+21
        Invoke(nameof(LateAwake),0f);
    }

    private void LateAwake()
    {
        UpdateValues();
        SetUpgradeUI(false);
    }

    public void UpdateValues()
    {
        var time= UpgradeSystem.Instance.GetValue(GeneralUpgradeType.AllStandSpeedUp, 1);
        time*= UpgradeSystem.Instance.GetValue(UpgradeType.StandSpeedUp, _standType, 1);
        currentCreationTime = baseCreationTime/time;
        // Debug.LogError("Creation time is:" + currentCreationTime);
        var price= UpgradeSystem.Instance.GetValue(GeneralUpgradeType.AllMultiplier, 1);
        price*= UpgradeSystem.Instance.GetValue(UpgradeType.StandMultiplier, _standType, 1);
        currentPrice = basePrice * price*currentPriceRate;

        upgradeUi.SetStandUpgrade(currentLevel, (int)currentPrice, currentCreationTime, (int)currentUpgradePrice, name);
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

    public void LevelUp()
    {
        currentUpgradePrice += baseUpgradePrice + currentUpgradePrice / 5;
        currentLevel += 1;
        currentPriceRate = (int)(currentPrice / 10) + 1;
        UpdateValues();
        upgradeUi.SetStandUpgrade(currentLevel, (int)currentPrice, currentCreationTime, (int)currentUpgradePrice, name);
    }
    public void SetUpgradeUI(bool isActive)
    {
        upgradeUi.gameObject.SetActive(isActive);
    }
}
