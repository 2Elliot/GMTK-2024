using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private string _mainMenuSceneName;
    
    public void StartButton() {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void SettingsButton() {
        
    }

    public void QuitButton() {
        Application.Quit();
    }
}
