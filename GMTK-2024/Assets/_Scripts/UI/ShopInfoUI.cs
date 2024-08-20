using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopInfoUI : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private PurchasableItem item;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private Color _cannotAffordColor;
    [SerializeField] private Color _canAffordColor;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Button nextDayButton;
    
    public delegate void OnPurchase(PurchasableItem item); 
    public delegate void OnNextDay();

    private void OnEnable() {
        _gameController = SingletonContainer.Instance.GameController;
    }

    public void ResetUI()
    {
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);
        itemName.text = "No Item";
        itemDescription.text = "Select an item to view more information.";
        itemPrice.text = "Cost: 0";
        purchaseButton.interactable = false;
    }
    public void SetItem(PurchasableItem item)
    {
        this.item = item;
        itemImage.sprite = item.Image32;
        itemImage.color = new Color(1, 1, 1, 1);
        itemName.text = item.ItemName;
        itemDescription.text = item.Description;
        itemPrice.text = $"Cost: {item.Price.ToString()}";
        if (_gameController.CanAfford(item.Price)) {
            purchaseButton.interactable = true;
            itemPrice.color = _canAffordColor;
        }
        else {
            purchaseButton.interactable = false;
            itemPrice.color = _cannotAffordColor;
        }
    }
    public void SetPurchaseButtonListener(OnPurchase onPurchase)
    {
        purchaseButton.onClick.AddListener(() => onPurchase(item));
    }
    public void SetNextDayButtonListener(OnNextDay onNextDay)
    {
        nextDayButton.onClick.AddListener(() => onNextDay());
    }
    private void OnDisable() {
        purchaseButton.onClick.RemoveAllListeners();
        nextDayButton.onClick.RemoveAllListeners();
    }
}
