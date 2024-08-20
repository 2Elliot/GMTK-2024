using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotPrefab : MonoBehaviour
{
    public PurchasableItem Item;
    [SerializeField] private Image _itemImage;
    [SerializeField] private Button _selectButton;
    
    public delegate void OnSelect(PurchasableItem item);

    private void Start() {
        _itemImage.color = new Color(1, 1, 1, 1);
    }

    public void SetItem(PurchasableItem item)
    {
        Item = item;
        _itemImage.sprite = item.Image16;
    }
    public void SetSelectButtonListener(OnSelect onSelect)
    {
        _selectButton.onClick.AddListener(() => onSelect(Item));
    }
    public void DisableButton()
    {
        _selectButton.interactable = false;
        _itemImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }
    private void OnDisable() 
    {
        _selectButton.onClick.RemoveAllListeners();
    }
}
