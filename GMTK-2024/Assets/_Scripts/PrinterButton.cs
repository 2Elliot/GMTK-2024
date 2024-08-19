using UnityEngine;

public class PrinterButton : ClickableSprite {
    private PrinterController _printerController;
    private Renderer _renderer;
    
    private enum ButtonType {
        Number,
        Check,
        Ex
    }

    [SerializeField] private ButtonType _buttonType;
    [SerializeField] private int _number;

    private void Start() {
        _printerController = SingletonContainer.Instance.PrinterController;
        _renderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnSpriteClicked() {
        if (_buttonType == ButtonType.Check) {
            if (SingletonContainer.Instance.GameController.CanSubmitPrinter) {
                GetComponent<SFXController>().PlayHappy();
            }
            else {
                GetComponent<SFXController>().PlaySad();
            }
        } else
        {
            GetComponent<SFXController>().PlaySound();
        }
        
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

        _renderer.enabled = true;
    }

    protected override void OnSpriteReleased() {
        _renderer.enabled = false;
    }
}
