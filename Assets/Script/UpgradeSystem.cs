using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeSystem : Singleton<UpgradeSystem>
{
    private List<IObjectUpgradeManager> objectUpgradeManagers=new List<IObjectUpgradeManager>();
    private List<IGeneralUpgradeManager> generalUpgradeManagers=new List<IGeneralUpgradeManager>();

    protected override void OnAwake()
    {
        var ar = GetComponents<IObjectUpgradeManager>();
        objectUpgradeManagers = new List<IObjectUpgradeManager>(ar);
        var arr = GetComponents<IGeneralUpgradeManager>();
        generalUpgradeManagers = new List<IGeneralUpgradeManager>(arr);
    }

    public float GetValue(UpgradeType upgradeType, StandType standType, float value)
    {
        foreach (var objectUpgradeManager in objectUpgradeManagers)
        {
            value=objectUpgradeManager.GetValue(upgradeType, standType, value);
        }

        return value;
    }

    public float GetValue(GeneralUpgradeType generalUpgradeType, float value)
    {
        foreach (var generalUpgradeManager in generalUpgradeManagers)
        {
            value=generalUpgradeManager.GetValue(generalUpgradeType, value);
        }
        return value;
    }
    
}

