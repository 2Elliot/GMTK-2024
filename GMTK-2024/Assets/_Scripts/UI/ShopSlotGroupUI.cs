using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlotGroupUI : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private List<ShopSlotPrefab> _slots;

    private void OnEnable() {
        _shopManager = SingletonContainer.Instance.ShopManager;
        _gameController = SingletonContainer.Instance.GameController;
    }

    public void SetItems(List<PurchasableItem> items) {
        RemoveAllSlots();
        print($"{items.Count}");
        for (int i = 0; i < items.Count; i++) {
            GameObject slot = Instantiate(_slotPrefab, transform);
            ShopSlotPrefab slotScript = slot.GetComponent<ShopSlotPrefab>();
            slotScript.SetItem(items[i]);
            slotScript.SetSelectButtonListener(OnSelectItemFromSlot);
            print($"Null: _gameController {_gameController == null} items[i] {items[i] == null}");
            _slots.Add(slotScript);
        }
    }
    private void OnSelectItemFromSlot(PurchasableItem item) { // slotprefab -> *shopslotgroupui* -> shopmanager
        _shopManager.SelectItemFromSlots(item);
    }
    public void DisableItem(PurchasableItem item) {
        foreach (var slot in _slots) {
            if (slot.Item == item) {
                slot.DisableButton();
            }
        }
    }
    public void RemoveAllSlots() {
        foreach (var slot in _slots) {
            Destroy(slot.gameObject);
        }
        _slots.Clear();
    }
}
