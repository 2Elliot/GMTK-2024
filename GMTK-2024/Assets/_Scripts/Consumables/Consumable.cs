using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Consumable : ClickableSprite
{
    public enum ConsumableActions
    {
        TimeSlow,
        WeighLess,
        Placeholder1
    }
    
    public ConsumableActions action;

    [FormerlySerializedAs("sprite")] [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprites;

    private GameController _gameController;
    
    protected override void Start()
    {
        base.Start();

        SetSpriteBasedOnAction();

        _gameController = FindObjectOfType<GameController>();
    }
    
    private void SetSpriteBasedOnAction()
    {
        switch (action)
        {
            case ConsumableActions.TimeSlow:
                _spriteRenderer.sprite = _sprites[0];
                break;
            case ConsumableActions.WeighLess:
                _spriteRenderer.sprite = _sprites[1];
                break;
            case ConsumableActions.Placeholder1:
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
            case ConsumableActions.TimeSlow:
                TimeSlow();
                break;
            case ConsumableActions.WeighLess:
                WeighLess();
                break;
            case ConsumableActions.Placeholder1:
                Placeholder1();
                break;
        }
    }

    protected override void Update()
    {
        base.Update();
        
        
    }

    private void TimeSlow()
    {
        // implemet slow time
    }

    private void WeighLess()
    {
        // make person object weigh less
    }

    private void Placeholder1()
    {
        // 
    }
}