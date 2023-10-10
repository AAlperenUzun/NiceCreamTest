using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Customer customerPrefab;
    public Cashier cashierPrefab;
    private List<Customer> customers=new List<Customer>();
    private List<Cashier> cashiers=new List<Cashier>();
    private float customerCount=1;
    private float cashierCount=1;
    public Transform entranceT;
    public Transform exitT;
    public List<OrderPoint> orderPoints;
    public List<OrderPoint> orderList=new List<OrderPoint>();
    public List<Stand> products;
    private UpgradeManager _upgradeManager;

    private void Start()
    {
        _upgradeManager = GetComponent<UpgradeManager>();
        foreach (var product in products)
        {
            product.Init(_upgradeManager);
        }
        CreateCustomer();
        CreateCashier();
    }

    public void CreateCustomer()
    {
        int count=customers.Count;
        for (int i=count; i < customerCount; i++)
        {
            var tmpCustomer=Instantiate(customerPrefab, entranceT.position, entranceT.rotation, transform.root);
            customers.Add(tmpCustomer);
            tmpCustomer.Init(this);
            tmpCustomer.orderPoint = orderPoints[0];
        }
    }

    public void DestroyCustomer(Customer customer)
    {
        customers.Remove(customer);
        Destroy(customer.gameObject);
        CreateCustomer();
    }
    public void CreateCashier()
    {
        int count=cashiers.Count;
        for (int i=count; i < customerCount; i++)
        {
            var tmpCashier=Instantiate(cashierPrefab, transform.position, transform.rotation, transform.root);
            tmpCashier.Init(this);
            cashiers.Add(tmpCashier);
        }
    }

    private void Update()
    {
        if (customers.Count>0)
        {
            foreach (var customer in customers)
            {
                customer.SetState();
            }
        }
        
        foreach (var cashier in cashiers)
        {
            cashier.SetState();
        }
    }
}
