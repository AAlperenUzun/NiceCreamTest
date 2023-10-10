using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgradeSystem : UpgradeSystem
{
    public Dictionary<string, float> ownedItemsMultiplier;

    private void Start()
    {
        ownedItemsMultiplier = new Dictionary<string, float>
        {
            {"BasicHat", 1.2f}
        };
    }

    public override void ApplyUpgrade(Product product)
    {
        if (product.productName == "FirstStand" && ownedItemsMultiplier.ContainsKey("BasicHat"))
        {
            product.currentPrice *= ownedItemsMultiplier["BasicHat"];
        }
    }
}

