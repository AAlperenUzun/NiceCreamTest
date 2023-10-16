using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [NonSerialized] public OrderPoint orderPoint;
    public enum State{GoingQueue, WaitToOrder, Ordering, WaitForOrder, TakeOrder, GoingToExit}

    public float speed=3f;
    public Transform orderUi;

    [NonSerialized] public State currentState;
    
    private GameManager _gameManager;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void SetState()
    {
        switch (currentState)
        {
            case State.GoingQueue:
                Move(orderPoint.customerP.position);
                if (transform.position==orderPoint.customerP.position)
                {
                    currentState = Customer.State.WaitToOrder;
                    _gameManager.orderList.Add(orderPoint);
                }
                break;
            case State.WaitToOrder:
                if (!orderPoint.wantedProduct)
                {
                    currentState = State.Ordering;
                }
                break;
            case State.Ordering:
                if (orderPoint.wantedProduct)
                {
                    currentState = State.WaitForOrder;
                
                    SetOrderUi(true);
                }
                break;
            case State.WaitForOrder:
                if (!orderPoint.wantedProduct)
                {
                    currentState = State.GoingToExit;
                    SetOrderUi(false);
                }
                break;
            case State.TakeOrder:
                break;
            case State.GoingToExit:
                Move(_gameManager.exitT.position);
                if (transform.position==_gameManager.exitT.position)
                {
                    _gameManager.DestroyCustomer(this);
                }
                break;
        }
    }
    private void Move(Vector3 goal)
    {
        transform.position = Vector3.MoveTowards(transform.position,goal, speed*Time.deltaTime);
    }

    private void SetOrderUi(bool isActive)
    {
        orderUi.gameObject.SetActive(isActive);
    }
}
