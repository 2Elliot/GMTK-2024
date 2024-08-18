using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrinterButton : ClickableSprite {
    private PrinterController _printerController;
    private enum ButtonType {
        Number,
        Check,
        Ex
    }

    [SerializeField] private ButtonType _buttonType;
    [SerializeField] private int _number;

    protected override void Start() {
        _printerController = SingletonContainer.Instance.PrinterController;
        
        base.Start();
    }

    protected override void OnSpriteClicked() {
        switch (_buttonType) {
            case ButtonType.Number:
                _printerController.UpdateGuess(_number);
                break;
            case ButtonType.Check:
                _printerController.SubmitGuess();
                break;
            case ButtonType.Ex:
                _printerController.DeleteGuess();
                break;
        }
    }
}
