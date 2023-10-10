using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPoint : MonoBehaviour
{
    public Transform customerP;
    public Transform cashierP;
    [NonSerialized] public Stand wantedProduct;

    public void StartTakeOrder()
    {
        
    }
    
    public void TakeOrder()
    {
        
    }

    public void Delivered()
    {
        wantedProduct = null;
    }
}
