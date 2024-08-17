using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterArrowControllerDeprecated : ClickableSprite {
    private RegisterController _registerController;
    [SerializeField] private int _amount;

    protected override void Start() {
        base.Start();
        // _registerController = SingletonContainer.Instance.RegisterController;
    }
    
    protected override void OnSpriteClicked() {
        _registerController.Add(_amount);
    }
}
