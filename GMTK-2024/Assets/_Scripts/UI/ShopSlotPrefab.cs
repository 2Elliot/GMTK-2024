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
    
    public void SetItem(PurchasableItem item)
    {
        Item = item;
        _itemImage.sprite = item.Image32;
    }
    public void SetSelectButtonListener(OnSelect onSelect)
    {
        _selectButton.onClick.AddListener(() => onSelect(Item));
    }
    public void DisableButton()
    {
        _selectButton.interactable = false;
    }
    private void OnDisable() 
    {
        _selectButton.onClick.RemoveAllListeners();
    }
}
