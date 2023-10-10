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

    private void Start()
    {
        productPrefab.currentPrice = productPrefab.basePrice;
        productPrefab.currentCreationTime = productPrefab.baseCreationTime;
    }

    public void StartMake(Cashier cashier)
    {
        currentCashier = cashier;
        productPrefab.currentCreationTime = 0;
        isMaking = true;
    }

    private void Making()
    {
        productPrefab.currentCreationTime += Time.deltaTime;
        if (productPrefab.baseCreationTime<productPrefab.currentCreationTime)
        {
            Made();
        }
    }

    private void Made()
    {
        isAvailable = true;
        isMaking = false;
        var p = Instantiate(productPrefab, currentCashier.handT);
        p.currentPrice = p.basePrice * 2;
        currentCashier.TakeFood(p);
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
