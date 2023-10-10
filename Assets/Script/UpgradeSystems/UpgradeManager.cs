using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager :MonoBehaviour, IObjectUpgradeManager
{
    public Dictionary<UpgradeType, Dictionary<StandType, Upgrade>> OwnedUpgrades;
    private void Start()
    {
        InitializeUpgrades();

        float totalMultiplierForStand1 = CalculateTotalMultiplierForStand(StandType.Stand1, UpgradeType.StandMultiplier);
        Debug.Log($"Total Multiplier for Stand1: {totalMultiplierForStand1}");
    }

    public void SetUpgrades(StandType standType ,UpgradeType upgradeType)
    {
        CalculateTotalMultiplierForStand(standType, upgradeType);
    }
    void InitializeUpgrades()
    {
        OwnedUpgrades = new Dictionary<UpgradeType, Dictionary<StandType, Upgrade>>();

        AddUpgrade(new Upgrade { Type = UpgradeType.StandMultiplier, StandType = StandType.Stand1, Value = 1.5f, Description = "Stand1 Multiplier x1.5" });
        AddUpgrade(new Upgrade { Type = UpgradeType.StandSpeedUp, StandType = StandType.Stand1, Value = 2f, Description = "Stand2 Speed x2" });
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        if (!OwnedUpgrades.ContainsKey(upgrade.Type))
            OwnedUpgrades[upgrade.Type] = new Dictionary<StandType, Upgrade>();

        OwnedUpgrades[upgrade.Type][upgrade.StandType] = upgrade;
    }

    public float CalculateTotalMultiplierForStand(StandType stand, UpgradeType _upgradeType)
    {
        float multiplier = 1f;

        foreach (var upgradeType in OwnedUpgrades.Keys)
        {
            if (OwnedUpgrades[upgradeType].ContainsKey(stand) && upgradeType==_upgradeType)
                multiplier *= OwnedUpgrades[upgradeType][stand].Value;
        }
        return multiplier;
    }

    public float GetValue(UpgradeType upgradeType, StandType standType, float value)
    {
        return value * CalculateTotalMultiplierForStand(standType, upgradeType);
    }
}