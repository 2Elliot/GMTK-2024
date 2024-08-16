using InputHandler;
using UnityEngine;
using UnityEngine.InputSystem;

// This is an example class on how to use my input system.
public class PlayerController : MonoBehaviour
{
  private PlayerInputActions _inputActions;
  private Vector2 _currentDirection;

  [SerializeField] private float _moveSpeed;

  private void Start() {
    // This MUST be called in start because of race conditions
    _inputActions = InputReader.Instance.InputActions;
    
    _inputActions.Player.Move.started += OnMoveStarted;
    _inputActions.Player.Move.performed += OnMovePerformed;
    _inputActions.Player.Move.canceled += OnMoveCanceled;
  }

  private void OnDisable() {
    // REMEMBER TO UNSUBSCRIBE
    // This has caused us serious issues in the past and is a pain to debug
    _inputActions.Player.Move.started -= OnMoveStarted;
    _inputActions.Player.Move.performed -= OnMovePerformed;
    _inputActions.Player.Move.canceled -= OnMoveCanceled;
  }

  private void Update() {
    if (_currentDirection != Vector2.zero) {
      MovePlayer(_currentDirection);
    }
  }

  private void OnMoveStarted(InputAction.CallbackContext context) {
    _currentDirection = context.ReadValue<Vector2>();
  }

  private void OnMovePerformed(InputAction.CallbackContext context) {
    _currentDirection = context.ReadValue<Vector2>();
  }

  private void OnMoveCanceled(InputAction.CallbackContext context) {
    _currentDirection = Vector2.zero;
  }

  private void MovePlayer(Vector2 direction) {
    transform.Translate(direction * _moveSpeed * Time.deltaTime);
  }
}