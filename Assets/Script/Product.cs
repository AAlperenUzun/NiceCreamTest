using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Product : MonoBehaviour
{
    public string productName = "DefaultProduct";
    [HideInInspector]
    public float currentPrice;
    public TMP_Text priceT;

    public void SetValues(string name, float price)
    {
        productName = name;
        currentPrice = price;
        priceT.text = ""+currentPrice;
        priceT.transform.parent.gameObject.SetActive(true);
    }

}

