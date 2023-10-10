using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private List<UpgradeSystem> upgradeSystems;

    private void Start()
    {
        upgradeSystems = new List<UpgradeSystem>(GetComponents<UpgradeSystem>());
    }

    public void ApplyAllUpgrades(Product product)
    {
        foreach (var upgradeSystem in upgradeSystems)
        {
            upgradeSystem.ApplyUpgrade(product);
        }
    }
}

