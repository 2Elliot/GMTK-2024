using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrinterController : ClickableSprite {
  private GameController _gameController;
  
  private int _currentGuess = 0;
  [SerializeField] private TextMeshProUGUI _text;

  protected override void Start() {
    _gameController = SingletonContainer.Instance.GameController;
    
    base.Start();
  }

  protected override void OnMouseHoverEnter() {
    GetComponent<SpriteRenderer>().enabled = false;
    transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    _text.gameObject.SetActive(true);
  }
  
  protected override void OnMouseHoverExit() {
    GetComponent<SpriteRenderer>().enabled = true;
    transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    _text.gameObject.SetActive(false);
  }

  public void UpdateGuess(int guess) {
    if (_currentGuess > 10) return;

    _currentGuess *= 10;
    _currentGuess += guess;

    _text.text = _currentGuess.ToString();
  }

  public void DeleteGuess() {
    _currentGuess /= 10;
    _text.text = _currentGuess.ToString();
  }

  public void SubmitGuess() {
    _gameController.SubmitGuess(_currentGuess);
  }

  public void Reset() {
    _currentGuess = 0;
    _text.text = _currentGuess.ToString();
  }
}
