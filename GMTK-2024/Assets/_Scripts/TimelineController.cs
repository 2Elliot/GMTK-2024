using System;
using System.Collections;
using UnityEngine;

public class TimelineController : MonoBehaviour {
    private GameController _gameController;
    private DialogueHandler _dialogueHandler;
    private DayController _dayController;

    [SerializeField] private Customer _boss1;

    private void Start() {
        _gameController = SingletonContainer.Instance.GameController;
        _dialogueHandler = SingletonContainer.Instance.DialogueHandler;
        _dayController = SingletonContainer.Instance.DayController;
    }
    
    public void BossStartDialogueDay1() {
        _gameController.GetNewCustomerOrNewDay();
    }
}
