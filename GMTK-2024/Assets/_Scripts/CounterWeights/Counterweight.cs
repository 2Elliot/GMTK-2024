using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CounterWeight : ClickableSprite
{
    private Vector3 Offset;

    [SerializeField] private Transform _offsetPoint;

    [HideInInspector] public CounterWeightManager manager;
    [HideInInspector] public int index; // index for where in counterweight script


    [SerializeField] private float weight;

    private bool addWeight = true;
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Offset = transform.position - _offsetPoint.position;
    }

    protected override void Update()
    {
        if (!manager.isObjectClicked)
            base.Update();
        if (!SpriteClicked)
        {
            manager.isObjectClicked = false;
            SnapPosition();
        }
        else if (SpriteClicked)
        {
            manager.isObjectClicked = true;
        }
        
    }


    protected override void OnSpriteHeld()
    {
        Vector2 mousePositionScreen = Mouse.current.position.ReadValue();
        Vector3 mousePositionWorld = MainCamera.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, 0.0f));
        mousePositionWorld.z = 0;

        SetPosition(mousePositionWorld);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position + Offset;
    }

    public void SnapPosition()
    {
        if (manager.scale != null)
        {
            float distance = Vector3.Distance(transform.position, manager.scale.transform.position);

            if (distance <= manager.chainSnapRadius)
            {
                transform.position = manager.scale.transform.position;
                ChangeScaleWeight();
                addWeight = false;
            }
            else
            {
                transform.position = manager.counterweightsTransform[index];
                ChangeScaleWeight();
                addWeight = true;
            }
        }
        else
        {
            transform.position = manager.counterweightsTransform[index];
        }
    }

    public void ChangeScaleWeight()
    {
        if (manager.scaleController._weights[1].weight > weight-1 && addWeight)
        {
            manager.scaleController._weights[1].weight -= weight;
        }
        else if (manager.scaleController._weights[1].weight < 1 && !addWeight)
        {
            manager.scaleController._weights[1].weight += weight;
        }
    }
    
}
