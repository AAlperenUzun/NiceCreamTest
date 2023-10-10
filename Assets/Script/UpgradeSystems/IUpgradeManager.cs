using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IUpgradeManager
{
    public void SetUpgrades();
    
}

public interface IObjectUpgradeManager
{
    float GetValue(UpgradeType upgradeType, StandType standType, float value);
}
public interface IGeneralUpgradeManager
{
    float GetValue(GeneralUpgradeType generalUpgradeType, float value);
}
