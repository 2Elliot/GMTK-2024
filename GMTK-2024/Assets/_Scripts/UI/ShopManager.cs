using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private CounterWeightManager _counterWeightManager;
    [SerializeField] private List<PurchasableItem> _shopItems;
    [SerializeField] private ShopInfoUI _shopInfoUI;
    [SerializeField] private ShopSlotGroupUI _shopSlotsGroup;
    [SerializeField] private CanvasGroup _shopCanvasGroup;
    [SerializeField] private TextMeshProUGUI _moneyText;

    private SFXController _sfxController;

    private void Start() {
        _sfxController = GetComponent<SFXController>();
        
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

        _moneyText.text = $"Money: {_gameController.Money}";
    }
    public void HideShop() {
        Debug.Log("Add effects to the shop disappearing here.");
        _shopCanvasGroup.alpha = 0;
        _shopCanvasGroup.interactable = false;
        _shopCanvasGroup.blocksRaycasts = false;

        SingletonContainer.Instance.PauseController.InShop = false;
    }
    public void SelectItemFromSlots(PurchasableItem item) {
        _sfxController.PlaySound();
        _shopInfoUI.SetItem(item);
    }
    public void PurchaseItemFromInfo(PurchasableItem item) {
        _sfxController.PlayHappy();
        _shopInfoUI.ResetUI();
        _shopSlotsGroup.DisableItem(item);
        _gameController.SpendMoney(item.Price);
        _counterWeightManager.Unlocks[item.UnlockIndex] = true;
        _moneyText.text = $"Money: {_gameController.Money}";
    }
    public void ContinueToNextDay() {
        _sfxController.PlaySound();
        HideShop();
        _gameController.GetNewCustomerOrNewDay();
    }
}
