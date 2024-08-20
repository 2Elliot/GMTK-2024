using Unity.VisualScripting;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _mainMenu;
    [SerializeField] private CanvasGroup _settingsMenu;
    
    public void BackButton() {
        _mainMenu.alpha = 1;
        _mainMenu.interactable = true;
        _mainMenu.blocksRaycasts = true;
        
        _settingsMenu.alpha = 0;
        _settingsMenu.interactable = false;
        _mainMenu.blocksRaycasts = false;
    }
    
    public void FullscreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
