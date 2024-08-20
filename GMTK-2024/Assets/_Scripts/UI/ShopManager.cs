using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private CounterWeightManager _counterWeightManager;
    [SerializeField] private List<PurchasableItem> _shopItems;
    [SerializeField] private ShopInfoUI _shopInfoUI;
    [SerializeField] private ShopSlotGroupUI _shopSlotsGroup;
    [SerializeField] private CanvasGroup _shopCanvasGroup;

    private void Start() {
        HideShop();
        _gameController = SingletonContainer.Instance.GameController;
        _counterWeightManager = SingletonContainer.Instance.CounterWeightManager;
        _shopSlotsGroup.SetItems(_shopItems);
        _shopInfoUI.ResetUI();
        _shopInfoUI.SetPurchaseButtonListener(PurchaseItemFromInfo);
        _shopInfoUI.SetNextDayButtonListener(ContinueToNextDay);
    }

    public void ShowShop() {
        Debug.Log("Add effects to the shop appearing here.");
        _shopCanvasGroup.alpha = 1;
        _shopCanvasGroup.interactable = true;
        _shopCanvasGroup.blocksRaycasts = true;
    }
    public void HideShop() {
        Debug.Log("Add effects to the shop disappearing here.");
        _shopCanvasGroup.alpha = 0;
        _shopCanvasGroup.interactable = false;
        _shopCanvasGroup.blocksRaycasts = false;
    }
    public void SelectItemFromSlots(PurchasableItem item) {
        _shopInfoUI.SetItem(item);
    }
    public void PurchaseItemFromInfo(PurchasableItem item) {
        _shopInfoUI.ResetUI();
        _shopSlotsGroup.DisableItem(item);
        _gameController.SpendMoney(item.Price);
        _counterWeightManager.Unlocks[item.UnlockIndex] = true;
    }
    public void ContinueToNextDay() {
        HideShop();
        _gameController.GetNewCustomerOrNewDay();
    }
}
