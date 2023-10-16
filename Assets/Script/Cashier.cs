using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cashier : MonoBehaviour
{
    [NonSerialized] public OrderPoint orderPoint;
    public enum State{Idle, GoingQueue, TakingOrder, GoingStand, MakingOrder, GoingToDeliver, DeliverOrder}

    public float speed=3f;

    [NonSerialized] public State currentState;
    private GameManager _gameManager;
    public float takeOrderTime = 1f;
    private float currentOrderingTime;
    private float currentCreationTime;
    public Transform handT;
    public Image image;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        image.transform.parent.LookAt(gameManager.camera.transform);
    }

    public void SetState()
    {
        switch (currentState)
        {
            case State.Idle:
                if (_gameManager.orderList.Count>0)
                {
                    orderPoint = _gameManager.orderList[0];
                    _gameManager.orderList.RemoveAt(0);
                    currentState = State.GoingQueue;
                }
                break;
            case State.GoingQueue:
                Move(orderPoint.cashierP.position);
                if (transform.position==orderPoint.cashierP.position)
                {
                    currentState = State.TakingOrder;
                    _gameManager.orderList.Add(orderPoint);
                    currentOrderingTime = 0;
                    image.fillAmount = 0;
                    image.transform.parent.gameObject.SetActive(true);
                }
                break;
            case State.TakingOrder:
                TakingOrder();
                break;
            case State.GoingStand:
                if (orderPoint.wantedProduct.isAvailable)
                {
                    Move(orderPoint.wantedProduct.transform.position);
                    if (transform.position==orderPoint.wantedProduct.transform.position)
                    {
                        currentState = State.MakingOrder;
                        orderPoint.wantedProduct.isAvailable = false;
                        orderPoint.wantedProduct.UpdateValues();
                        image.fillAmount = 0;
                        image.transform.parent.gameObject.SetActive(true);
                        currentCreationTime = 0;
                    }
                }
                break;
            case State.MakingOrder:
                MakingOrder();
                break;
            case State.GoingToDeliver:
                Move(orderPoint.cashierP.position);
                if (transform.position==orderPoint.cashierP.position)
                {
                    currentState = State.DeliverOrder;
                }
                break;
            case State.DeliverOrder:
                orderPoint.Delivered();
                Destroy(handT.GetChild(0).gameObject);
                _gameManager.orderList.Remove(orderPoint);
                currentState = State.Idle;
                break;
        }
    }

    private void Move(Vector3 goal)
    {
        transform.position = Vector3.MoveTowards(transform.position,goal, speed*Time.deltaTime);
    }
    private void TakingOrder()
    {
        currentOrderingTime += Time.deltaTime;
        image.fillAmount = currentOrderingTime / takeOrderTime;
        if (currentOrderingTime>=takeOrderTime)
        {
            orderPoint.wantedProduct=_gameManager.products[UnityEngine.Random.Range(0, 2)];
            ChangeState(State.GoingStand);
            image.transform.parent.gameObject.SetActive(false);
        }
    }

    private void MakingOrder()
    {
        currentCreationTime += Time.deltaTime;
        image.fillAmount = currentCreationTime / orderPoint.wantedProduct.currentCreationTime;
        if (orderPoint.wantedProduct.currentCreationTime<=currentCreationTime)
        {
            var product = orderPoint.wantedProduct.Made();
            product.transform.parent = handT;
            product.transform.localPosition = Vector3.zero;
            ChangeState(State.GoingToDeliver);
            image.transform.parent.gameObject.SetActive(false);
        }
    }
    private void ChangeState(State targetState)
    {
        currentState = targetState;
    }
}
