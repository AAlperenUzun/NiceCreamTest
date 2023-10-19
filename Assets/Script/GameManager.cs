using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [NonSerialized] public Camera camera;
    public LayerMask interactableLayer;
    private Stand currentStand;
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
        if (customers.Count > 0)
        {
            foreach (var customer in customers.Where(customer => customer))
            {
                customer.SetState();
            }
        }

        foreach (var cashier in cashiers)
        {
            cashier.SetState();
        }
        
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                CheckRaycast(touch.position);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            CheckRaycast(Input.mousePosition);
        }
    }
    
    void CheckRaycast(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            if (hit.transform.TryGetComponent(out Stand stand))
            {
                if (currentStand && currentStand !=stand)
                {
                    currentStand.SetUpgradeUI(false);
                }
                currentStand = stand;
                stand.SetUpgradeUI(true);
            }
        }else if (currentStand)
        {
            if (IsPointerOverUI(position))return;
            currentStand.SetUpgradeUI(false);
            currentStand = null;
        }
    }
    bool IsPointerOverUI(Vector2 screenPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPosition;
    
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }
}
