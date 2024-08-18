using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    //access days
    public List<Day> days;
    //access people
    public List<Customer> customers;
    //current customer
    public Customer currentCustomer;
    //something to keep track of the current customer
    private int cCount = 0;
    private int dCount = 0;

    private GameController _controller;

    //list of days - done
    //list of customers (for the day) - done
    //function to generate people for each day - done
    //function to iterate to the next person - done

    private void Start() {
        SingletonContainer instance = SingletonContainer.Instance;
        _controller = instance.GameController;
    }
    
    public void StartGame()
    {
        NewDay(dCount);
    }
    
    private void NewDay(int day)
    {
        customers = days[day].Customers;
        customers.MMShuffle();
        currentCustomer = customers[cCount];
        _controller.NewCustomer(currentCustomer);
        dCount++;
    }

    public void NextCustomer()
    {
        cCount++;
        if (cCount < customers.Count)
        {
            currentCustomer = customers[cCount];
            _controller.NewCustomer(currentCustomer); 
        }
        else
        {
            Debug.Log("ADD CUSTOMERS TO DAYS!!! PLEASE!!!!!!!!!!!!");
            cCount = 0;
            if (dCount > days.Count)
            {
                NewDay(dCount);
            }
            else
            {
                Debug.Log("OUT OF DAYS!!!!!");
            }
        }
    }
}
