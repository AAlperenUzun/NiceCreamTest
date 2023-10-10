using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour, IGeneralUpgradeManager
{
    public event Action<GeneralUpgradeType, float> Upgraded;
    
    public Dictionary<GeneralUpgradeType, Upgrade>  OwnedEquipments;
    private void Start()
    {
        InitializeUpgrades();

        float totalMultiplier = CalculateTotalMultiplier(GeneralUpgradeType.AllMultiplier);
        Debug.Log($"Total Multiplier for Stand1: {totalMultiplier}");
    }

    void InitializeUpgrades()
    {
        OwnedEquipments = new Dictionary<GeneralUpgradeType, Upgrade>();


        AddUpgrade(new Upgrade { generalType = GeneralUpgradeType.AllMultiplier, StandType = StandType.All, Value = 2f, Description = "All Profit x2" });
        AddUpgrade(new Upgrade { generalType = GeneralUpgradeType.AllStandSpeedUp, StandType = StandType.All, Value = 1.3f, Description = "+%30 Food made faster" });
    }

    public void AddUpgrade( Upgrade upgrade)
    {
        if (!OwnedEquipments.ContainsKey(upgrade.generalType))
            OwnedEquipments = new Dictionary<GeneralUpgradeType, Upgrade>();

        OwnedEquipments[upgrade.generalType] = upgrade;
        
        Upgraded?.Invoke(upgrade.generalType, CalculateTotalMultiplier(upgrade.generalType));
    }

    public float CalculateTotalMultiplier(GeneralUpgradeType _gUpgradeType)
    {
        float multiplier = 1f;

        foreach (var gUpgradeType in OwnedEquipments.Keys)
        {
            if (gUpgradeType==_gUpgradeType)
                multiplier *= OwnedEquipments[gUpgradeType].Value;
        }

        return multiplier;
    }


    public float GetValue(GeneralUpgradeType generalUpgradeType, float value)
    {
        return value * CalculateTotalMultiplier(generalUpgradeType);
    }
}