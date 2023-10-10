using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public string productName = "DefaultProduct";
    [HideInInspector]
    public float currentPrice;

    public void SetValues(string name, float price)
    {
        productName = name;
        currentPrice = price;
    }

}

