using System;
using InputHandler;
using UnityEngine.InputSystem;
using UnityEngine;

public class WeightController : MonoBehaviour {
    
    private PlayerInputActions _inputActions;
    private Camera _mainCamera;
    
    [SerializeField] private Transform _connectionPoint;

    private Vector3 offset;

    private bool _spriteClicked;

    public int Index;
    public ScaleController scaleController;
    
    private void Start() {
        _inputActions = InputReader.Instance.InputActions;

        _inputActions.Player.Mouse0.performed += OnClick;
        _inputActions.Player.Mouse0.canceled += _ => { _spriteClicked = false; };

        _mainCamera = Camera.main;
        
        offset = transform.position - _connectionPoint.position;
    }

    private void OnDisable() {
        _inputActions.Player.Mouse0.performed -= OnClick;
    }

    public void SetPosition(Vector3 position) {
        transform.position = position + offset;
    }
    
    private void OnClick(InputAction.CallbackContext context) {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        
        if (hit.collider != null) {
            if (hit.collider.gameObject == gameObject) {
                _spriteClicked = true;
            }
        }
    }

    private void Update() {
        if (_spriteClicked) {
            Vector2 mousePositionScreen = Mouse.current.position.ReadValue();
            Vector3 mousePositionWorld = _mainCamera.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, 0));
        
            scaleController.SetWeightXPosition(Index, mousePositionWorld);
        }
    }

}
