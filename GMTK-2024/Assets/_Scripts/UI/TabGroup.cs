using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public Sprite tabDisabled;

    private void Start() {
        if (tabButtons.Count > 0) {
            ResetTabs();
            tabButtons[0]._isSelected = true;
            OnTabSelect(tabButtons[0]);
        }
    }


    public void AttachToGroup(TabButton tabButton)
    {
        tabButtons.Add(tabButton);
    }
    
    private bool Check(TabButton tabButton)
    {
        if (tabButton._isDisabled) {
            OnTabUpdateWhileDisabled(tabButton);
            return true;
        }
        return false;
    }
    public void OnTabEnter(TabButton tabButton) {
        if (Check(tabButton)) return;
        tabButton._tabBackground.sprite = tabHover;
    }
    public void OnTabSelect(TabButton tabButton)
    {
        if (Check(tabButton)) return;
        tabButton._isSelected = true;
        tabButton._tabBackground.sprite = tabActive;
        tabButton.ShowContent();
        ResetTabs();
    }
    public void OnTabExit(TabButton tabButton)
    {
        if (Check(tabButton)) return;
        tabButton._tabBackground.sprite = tabIdle;
    }
    private void OnTabUpdateWhileDisabled(TabButton tabButton)
    {
        tabButton._tabBackground.sprite = tabDisabled;
    }
    
    private void ResetTabs()
    {
        foreach (TabButton tabButton in tabButtons)
        {
            if (tabButton._isDisabled || tabButton._isSelected) {
                continue;
            }
            tabButton._tabBackground.sprite = tabIdle;
            tabButton._isSelected = false;
            tabButton.HideContent();
        }
    }
}
