using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterAcceptorControllerDeprecated : ClickableSprite
{
    private RegisterController _registerController;

    protected override void Start() {
        base.Start();
        // _registerController = SingletonContainer.Instance.RegisterController;
    }
    
    protected override void OnSpriteClicked() {
        _registerController.Submit();
    }
}
