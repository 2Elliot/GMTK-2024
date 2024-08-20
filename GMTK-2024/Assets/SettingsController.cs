using System;
using MoreMountains.Tools;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _mainMenu;
    [SerializeField] private CanvasGroup _settingsMenu;
    
    private SFXController _sfxController;

    private void Start() {
        _sfxController = GetComponent<SFXController>();
    }

    public void BackButton() {
        MMSoundManagerEvent.Trigger(MMSoundManagerEventTypes.SaveSettings);
        
        _mainMenu.alpha = 1;
        _mainMenu.interactable = true;
        _mainMenu.blocksRaycasts = true;
        
        _settingsMenu.alpha = 0;
        _settingsMenu.interactable = false;
        _settingsMenu.blocksRaycasts = false;
        
        _sfxController.PlaySound();
    }

    public void Fullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;

        _sfxController.PlaySound();
    }

    private void OnEnable() {
        _mainMenu.alpha = 1;
        _mainMenu.interactable = true;
        _mainMenu.blocksRaycasts = true;
        
        _settingsMenu.alpha = 0;
        _settingsMenu.interactable = false;
        _settingsMenu.blocksRaycasts = false;
    }
}
