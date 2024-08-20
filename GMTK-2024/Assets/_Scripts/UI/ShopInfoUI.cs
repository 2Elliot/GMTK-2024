using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopInfoUI : MonoBehaviour
{
    [SerializeField] private PurchasableItem item;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Button nextDayButton;
    
    public delegate void OnPurchase(PurchasableItem item); 
    public delegate void OnNextDay();
    
    public void ResetUI()
    {
        itemImage.sprite = null;
        itemName.text = "No Item";
        itemDescription.text = "Select an item to view more information.";
        itemPrice.text = "Cost: 0";
        purchaseButton.interactable = false;
    }
    public void SetItem(PurchasableItem item)
    {
        this.item = item;
        itemImage.sprite = item.Image32;
        itemName.text = item.ItemName;
        itemDescription.text = item.Description;
        itemPrice.text = $"Cost: {item.Price.ToString()}";
        purchaseButton.interactable = true;
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
