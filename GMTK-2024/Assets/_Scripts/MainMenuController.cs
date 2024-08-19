using System;
using System.Collections;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private string _mainMenuSceneName;

    [SerializeField] private Crossfade _crossfade;
    [SerializeField] private MusicController _musicController;
    private SFXController _sfxController;

    private void Start() {
        _sfxController = GetComponent<SFXController>();
        
        _musicController.PlayMusic();
    }

    public void StartButton()
    {
        _sfxController.PlaySound();
        StartCoroutine(_crossfade.PlayTransition(this, nameof(OnStartButtonTransitionEnd)));
    }

    public void SettingsButton()
    {
        _sfxController.PlaySound();
        StartCoroutine(_crossfade.PlayTransition(this, nameof(OnSettingsButtonTransitionEnd)));
    }

    public void QuitButton()
    {
        _sfxController.PlaySound();
        StartCoroutine(_crossfade.PlayTransition(this, nameof(QuitGame)));
    }

    
    
    
    public void OnStartButtonTransitionEnd()
    {
        // Load the main menu scene
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void OnSettingsButtonTransitionEnd()
    {
        // open settings menu
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    
    
    
    
}
