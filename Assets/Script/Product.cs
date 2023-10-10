using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public string productName = "DefaultProduct";
    public float basePrice = 10.0f;
    [HideInInspector]
    public float currentPrice;
    public float baseCreationTime = 5.0f;
    [HideInInspector]
    public float currentCreationTime;

}

