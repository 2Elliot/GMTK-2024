using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour {
    [SerializeField] private List<Day> _days;
    public Day _currentDay;
    private List<Customer> _customers;
    
    private int _currentDayIndex = -1; // Must start before day 0
    private int _currentCustomerIndex;
    
    public void NewDay() {
        Reset();

        _currentDayIndex++;
        _currentDay = _days[_currentDayIndex];

        int[] customerIndices = Enumerable.Range(0, _currentDay.Customers.Count).ToArray();

        // Shuffle the values in customerIndices
        System.Random rng = new();
        customerIndices = customerIndices.OrderBy(x => rng.Next()).ToArray();

        foreach (int t in customerIndices) {
            _customers.Add(_currentDay.Customers[t]);
        }
    }

    public Customer FetchNewCustomer() {
        if (_currentCustomerIndex >= _customers.Count) {
            Debug.Log("New Day");
            NewDay();
            return null; // Day over
        }

        if (_currentCustomerIndex == -1) {
            Debug.Log("Boss");
            _currentCustomerIndex++;
            return _currentDay.Boss;
        }
        else {
            Debug.Log($"Customer: {_currentCustomerIndex}");
            return _customers[_currentCustomerIndex++];
        }
    }

    public void Reset() {
        _customers.Clear();

        _currentCustomerIndex = -1; // Start with boss
    }
}
