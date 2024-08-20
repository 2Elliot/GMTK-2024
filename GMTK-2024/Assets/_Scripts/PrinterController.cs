using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrinterController : ClickableSprite {
  private GameController _gameController;
  
  private int _currentGuess = 0;

  [SerializeField] private GameObject _downAsset;
  [SerializeField] private GameObject _upAsset;
  
  [SerializeField] private TextMeshProUGUI _text;

  [SerializeField] private Animator _animator;

  private void Start() {
    _gameController = SingletonContainer.Instance.GameController;
  }

  protected override void OnMouseHoverEnter() {
    if (!_gameController.CanSubmitPrinter) return;
    
    _downAsset.SetActive(false);
    _upAsset.SetActive(true);
    _text.gameObject.SetActive(true);
  }
  
  protected override void OnMouseHoverExit() {
    _downAsset.SetActive(true);
    _upAsset.SetActive(false);
    _text.gameObject.SetActive(false);
  }

  public void UpdateGuess(int guess) {
    if (_currentGuess > 99) return;

    _currentGuess *= 10;
    _currentGuess += guess;

    _text.text = _currentGuess.ToString();
  }

  public void DeleteGuess() {
    _currentGuess /= 10;
    _text.text = _currentGuess.ToString();
  }

  public void SubmitGuess() {
    if (_gameController.CanSubmitPrinter) _animator.SetTrigger("StartAnimation");
    
    _gameController.SubmitGuess(_currentGuess);
  }

  public void Reset() {
    _currentGuess = 0;
    _text.text = _currentGuess.ToString();
  }
}
