using InputHandler;
using UnityEngine.InputSystem;
using UnityEngine;

public class ClickableSprite : MonoBehaviour {
    
  protected PlayerInputActions InputActions;
  protected Camera MainCamera;

  protected bool SpriteClicked;
  protected bool IsHovering;

  // LayerMask to specify which layers to ignore
  public LayerMask IgnoreLayerMask;

  protected virtual void OnEnable() {
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
        
    RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, ~IgnoreLayerMask);
        
    if (hit.collider != null && hit.collider.gameObject == gameObject) {
      SpriteClicked = true;
      OnSpriteClicked();
    }
  }

  protected virtual void Update() {
    Vector2 mousePosition = Mouse.current.position.ReadValue();
    Vector2 worldPosition = MainCamera.ScreenToWorldPoint(mousePosition);

    RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, ~IgnoreLayerMask);

    if (hit.collider != null && hit.collider.gameObject == gameObject) {
      if (!IsHovering) {
        IsHovering = true;
        OnMouseHoverEnter();
      }
    } else {
      if (IsHovering) {
        IsHovering = false;
        OnMouseHoverExit();
      }
    }

    if (SpriteClicked) {
      OnSpriteHeld();
    }
  }

  protected virtual void  OnSpriteClicked() {
    // Override this method in derived classes for specific behavior on click
  }

  protected virtual void OnSpriteReleased() {
    // Override this method in derived class for specific behavior on release
  }

  protected virtual void OnSpriteHeld() {
    // Override this method in derived classes for specific behavior when sprite is held
  }

  protected virtual void OnMouseHoverEnter() {
    // Override this method in derived classes for specific behavior on hover enter
  }

  protected virtual void OnMouseHoverExit() {
    // Override this method in derived classes for specific behavior on hover exit
  }
}
