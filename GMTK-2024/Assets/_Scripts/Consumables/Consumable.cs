using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Consumable : ClickableSprite
{
    public enum ConsumableActions
    {
        GainTime,
        WeighLess,
        Skip
    }
    
    public ConsumableActions action;

    [FormerlySerializedAs("sprite")] [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprites;

    private GameController _gameController;
    private ScaleController _scaleController;


    private float _gainTimeAmount = 10;
    private float _weighLessAmount = 2; // this is devided
    
    
    
    
    private void Start()
    {
        SetSpriteBasedOnAction();

        _gameController = SingletonContainer.Instance.GameController;
        _scaleController = SingletonContainer.Instance.ScaleController;
    }
    
    private void SetSpriteBasedOnAction()
    {
        switch (action)
        {
            case ConsumableActions.GainTime:
                _spriteRenderer.sprite = _sprites[0];
                break;
            case ConsumableActions.WeighLess:
                _spriteRenderer.sprite = _sprites[1];
                break;
            case ConsumableActions.Skip:
                _spriteRenderer.sprite = _sprites[2];
                break;
            default:
                Debug.LogWarning("No sprite assigned for the selected action.");
                break;
        }
    }
    
    protected override void OnSpriteClicked()
    {
        switch (action)
        {
            case ConsumableActions.GainTime:
                GainTime();
                Debug.Log("yes");
                break;
            case ConsumableActions.WeighLess:
                WeighLess();
                break;
            case ConsumableActions.Skip:
                Skip();
                break;
        }
    }

    private void GainTime()
    {
        _gameController.StartGuessTime -= _gainTimeAmount;
        Destroy();
    }

    private void WeighLess()
    {
        _scaleController._weights[0].weight /= _weighLessAmount;
        Destroy();
    }

    private void Skip()
    {
        // add later
    }



    
    
    private void Destroy()
    {
        Destroy(gameObject);
    }
}