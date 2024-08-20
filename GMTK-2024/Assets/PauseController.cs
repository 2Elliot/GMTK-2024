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
    
    private void Start()
    {
        InputActions = InputReader.Instance.InputActions;
        _sfxController = GetComponent<SFXController>();
        
        InputActions.Player.Pause.performed += _ => {
            PauseToggle();
        };
    }

    private void PauseToggle() {
        if (Paused) { // Unpause
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
        _sfxController.PlaySound();
        
        _mainMenu.alpha = 0;
        _mainMenu.interactable = false;
        _mainMenu.blocksRaycasts = false;
        
        _settingsMenu.alpha = 1;
        _settingsMenu.interactable = true;
        _settingsMenu.blocksRaycasts = true;
    }

    public void QuitButton() {
        _sfxController.PlaySound();
        SceneManager.LoadScene("MainMenu");
    }
}
