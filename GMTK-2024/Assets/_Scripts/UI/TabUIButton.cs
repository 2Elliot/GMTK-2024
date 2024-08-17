using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TabUIButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup _tabGroup;
    public SpriteRenderer _tabBackground;
    public GameObject _tabContent;
    public bool _isSelected = false;
    public bool _isDisabled = false;

    private void Start() 
    { 
        _tabGroup.AttachToGroup(this);
    }
    
    public void ShowContent()
    {
        _tabContent.SetActive(true);
    }
    public void HideContent()
    {
        _tabContent.SetActive(false);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _tabGroup.OnTabSelect(this);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _tabGroup.OnTabEnter(this);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        _tabGroup.OnTabExit(this);
    }
}
