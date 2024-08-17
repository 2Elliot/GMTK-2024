using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabUIButton> tabButtons;
    
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


    public void AttachToGroup(TabUIButton tabUIButton)
    {
        tabButtons.Add(tabUIButton);
    }
    
    private bool Check(TabUIButton tabUIButton)
    {
        if (tabUIButton._isDisabled) {
            OnTabUpdateWhileDisabled(tabUIButton);
            return true;
        }
        return false;
    }
    public void OnTabEnter(TabUIButton tabUIButton) {
        if (Check(tabUIButton)) return;
        tabUIButton._tabBackground.sprite = tabHover;
    }
    public void OnTabSelect(TabUIButton tabUIButton)
    {
        if (Check(tabUIButton)) return;
        tabUIButton._isSelected = true;
        tabUIButton._tabBackground.sprite = tabActive;
        tabUIButton.ShowContent();
        ResetTabs();
    }
    public void OnTabExit(TabUIButton tabUIButton)
    {
        if (Check(tabUIButton)) return;
        tabUIButton._tabBackground.sprite = tabIdle;
    }
    private void OnTabUpdateWhileDisabled(TabUIButton tabUIButton)
    {
        tabUIButton._tabBackground.sprite = tabDisabled;
    }
    
    private void ResetTabs()
    {
        foreach (TabUIButton tabButton in tabButtons)
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
