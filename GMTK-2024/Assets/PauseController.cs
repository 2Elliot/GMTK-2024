using System;
using InputHandler;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private PlayerInputActions InputActions;

    [SerializeField] private GameObject _pauseMenu;
    
    public bool Paused; 
    
    private void Start()
    {
        InputActions = InputReader.Instance.InputActions;

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
}
