using System;
using UnityEngine;

public class TimelineController : MonoBehaviour {
    private GameController _gameController;
    private DialogueHandler _dialogueHandler;

    [SerializeField] private Customer _boss1;

    private void Start() {
        _gameController = SingletonContainer.Instance.GameController;
        _dialogueHandler = SingletonContainer.Instance.DialogueHandler;
    }

    public void BossStartDialogueDay1() {
        _gameController.NewCustomer(_boss1);
    }
    
    public void BossItemDay1() {
        _gameController.NewCustomer(_boss1);
    }

    public void StartDay1() {
        // _gameController.NewCustomer();
    }
}
