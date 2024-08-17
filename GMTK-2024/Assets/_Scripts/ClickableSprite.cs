using System;
using InputHandler;
using UnityEngine.InputSystem;
using UnityEngine;

public class ClickableSprite : MonoBehaviour {
    
  protected PlayerInputActions InputActions;
  protected Camera MainCamera;

  protected bool SpriteClicked;
  
  protected virtual void Start() {
    InputActions = InputReader.Instance.InputActions;

    InputActions.Player.Mouse0.performed += OnClick;
    InputActions.Player.Mouse0.canceled += _ => { 
      if (SpriteClicked) {
        SpriteClicked = false; 
        OnSpriteReleased();
      }
    };

    MainCamera = Camera.main;
  }

  protected virtual void OnDisable() {
    InputActions.Player.Mouse0.performed -= OnClick;
  }
    
  protected virtual void OnClick(InputAction.CallbackContext context) {
    Vector2 mousePosition = Mouse.current.position.ReadValue();
    Vector2 worldPosition = MainCamera.ScreenToWorldPoint(mousePosition);
        
    RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        
    if (hit.collider != null && hit.collider.gameObject == gameObject) {
      SpriteClicked = true;
      OnSpriteClicked();
    }
  }

  protected virtual void OnSpriteClicked() {
    // Override this method in derived classes for specific behavior on click
  }

  protected virtual void OnSpriteReleased() {
    // Override this method in derived class for specific behavior on release
  }

  protected virtual void Update() {
    if (SpriteClicked) {
      OnSpriteHeld();
    }
  }

  protected virtual void OnSpriteHeld() {
    // Override this method in derived classes for specific behavior when sprite is held
  }
}