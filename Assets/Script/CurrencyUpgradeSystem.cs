using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyUpgradeSystem : UpgradeSystem
{
    public Dictionary<string, float> purchasedUpgradesMultiplier;

    private void Start()
    {
        purchasedUpgradesMultiplier = new Dictionary<string, float>
        {
            {"FirstStandMultiplier", 1.5f},
            {"SecondStandTimeReducer", 0.7f}
        };
    }

    public override void ApplyUpgrade(Product product)
    {
        if (product.productName == "FirstStand" && purchasedUpgradesMultiplier.ContainsKey("FirstStandMultiplier"))
        {
            product.currentPrice *= purchasedUpgradesMultiplier["FirstStandMultiplier"];
        }
        else if (product.productName == "SecondStand" && purchasedUpgradesMultiplier.ContainsKey("SecondStandTimeReducer"))
        {
            product.currentCreationTime *= purchasedUpgradesMultiplier["SecondStandTimeReducer"];
        }
    }
}

