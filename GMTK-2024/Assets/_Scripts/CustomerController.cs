using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerController : MonoBehaviour {
    [SerializeField] private ScaleController _scaleController;
    [SerializeField] private SpriteRenderer _customerImageRenderer;
    [SerializeField] private TextMeshProUGUI _customerName;

    [SerializeField] private List<Customer> _customers;
    private int _previousCustomerId = -1;

    private Customer _currentCustomer;
    private Item _currentItem;
    
    public void NewCustomer() {
        ChooseNewCustomer();

        ChooseNewItem();

        _scaleController.ResetScale(_currentItem);
    }

    private void ChooseNewCustomer() {
        int currentCustomerID = -1;

        currentCustomerID = Random.Range(0, _customers.Count);
        while (currentCustomerID == _previousCustomerId) {
            currentCustomerID = Random.Range(0, _customers.Count);
        }
        _previousCustomerId = currentCustomerID;

        _currentCustomer = _customers[currentCustomerID];

        _customerImageRenderer.sprite = _currentCustomer.Image;
        _customerName.text = _currentCustomer.Name;
    }

    private void ChooseNewItem() {
        List<Item> items = _currentCustomer.Items;

        int itemIndex = Random.Range(0, items.Count);
        _currentItem = items[itemIndex];
    }
}
