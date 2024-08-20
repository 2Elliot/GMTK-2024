using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private string _mainMenuSceneName;

    [SerializeField] private Crossfade _crossfade;
    [SerializeField] private MusicController _musicController;
    private SFXController _sfxController;
    
    [SerializeField] private CanvasGroup _mainMenu;
    [SerializeField] private CanvasGroup _settingsMenu;

    private void Start() {
        _sfxController = GetComponent<SFXController>();
        
        _musicController.PlayMusic();
    }

    public void StartButton()
    {
        _sfxController.PlaySound();
        OnStartButtonTransitionEnd();
        // StartCoroutine(_crossfade.PlayTransition(this, nameof(OnStartButtonTransitionEnd)));
    }

    public void SettingsButton()
    {
        _sfxController.PlaySound();
        OnSettingsButtonTransitionEnd();
        // StartCoroutine(_crossfade.PlayTransition(this, nameof(OnSettingsButtonTransitionEnd)));
    }

    public void QuitButton()
    {
        _sfxController.PlaySound();
        QuitGame();
        // StartCoroutine(_crossfade.PlayTransition(this, nameof(QuitGame)));
    }
    
    public void OnStartButtonTransitionEnd()
    {
        // Load the main menu scene
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void OnSettingsButtonTransitionEnd()
    {
        _mainMenu.alpha = 0;
        _mainMenu.interactable = false;
        _mainMenu.blocksRaycasts = false;
        
        _settingsMenu.alpha = 1;
        _settingsMenu.interactable = true;
        _settingsMenu.blocksRaycasts = true;
        // open settings menu
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
