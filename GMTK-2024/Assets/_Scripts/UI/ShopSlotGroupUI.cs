using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlotGroupUI : MonoBehaviour
{
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private List<ShopSlotPrefab> _slots;
    
    public void SetItems(List<PurchasableItem> items) {
        RemoveAllSlots();
        for (int i = 0; i < items.Count; i++) {
            GameObject slot = Instantiate(_slotPrefab, transform);
            ShopSlotPrefab slotScript = slot.GetComponent<ShopSlotPrefab>();
            slotScript.SetItem(items[i]);
            slotScript.SetSelectButtonListener(OnSelectItemFromSlot);
            _slots.Add(slotScript);
        }
    }
    public void OnSelectItemFromSlot(PurchasableItem item) {
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
