using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UniqueActiveIndividual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Assign a UniqueActiveGroup to this script")]
    [SerializeField] private UniqueActiveGroup _group;
    [Header("Add objects here to be activated/deactivated when this is hovered over")]
    [SerializeField] private List<GameObject> _objects;
   
    private void OnEnable() {
        _group = GetComponentInParent<UniqueActiveGroup>();
        _group.AttachToGroup(this);
    }

    public void ActivateObjects() {
        foreach (var obj in _objects) {
            obj.SetActive(true);
        }
    }
    
    public void DisableObjects() {
        foreach (var obj in _objects) {
            obj.SetActive(false);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        _group.OnPointerEnterFor(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _group.ResetObjects();
    }
}
