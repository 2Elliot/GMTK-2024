using System;
using InputHandler;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private PlayerInputActions InputActions;

    [SerializeField] private GameObject _pauseMenu;
    private SFXController _sfxController;
    
    [SerializeField] private CanvasGroup _mainMenu;
    [SerializeField] private CanvasGroup _settingsMenu;
    
    public bool Paused;
    public bool InShop;
    
    private void Start()
    {
        InputActions = InputReader.Instance.InputActions;
        _sfxController = GetComponent<SFXController>();
        
        InputActions.Player.Pause.performed += _ => {
            PauseToggle();
        };
    }

    private void OnEnable() {
        Paused = false;
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }

    private void PauseToggle() {
        if (Paused) { // Unpause
            UnSettingsButton();
            
            Paused = false;
            Debug.Log("Unpaused");
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
        }
        else { // Pause
            Paused = true;
            Debug.Log("Paused");
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }
    }

    public void PauseButton() {
        _sfxController.PlaySound();
        PauseToggle();
    }

    public void SettingsButton() {
        Debug.Log("Settings");
        
        _sfxController.PlaySound();
        
        _mainMenu.alpha = 0;
        _mainMenu.interactable = false;
        _mainMenu.blocksRaycasts = false;
        
        _settingsMenu.alpha = 1;
        _settingsMenu.interactable = true;
        _settingsMenu.blocksRaycasts = true;
    }

    private void UnSettingsButton() {
        _mainMenu.alpha = 1;
        _mainMenu.interactable = true;
        _mainMenu.blocksRaycasts = true;
        
        _settingsMenu.alpha = 0;
        _settingsMenu.interactable = false;
        _settingsMenu.blocksRaycasts = false;
    }

    public void QuitButton() {
        _sfxController.PlaySound();
        SceneManager.LoadScene("MainMenu");
    }
}
