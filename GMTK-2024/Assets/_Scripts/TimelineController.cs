using System;
using UnityEngine;

public class TimelineController : MonoBehaviour {
    private GameController _gameController;

    private void Start() {
        _gameController = SingletonContainer.Instance.GameController;
    }

    public void BossStartDialogueDay1() {
        Debug.Log("Boss Dialogue");
    }
    
    public void BossItemDay1() {
        Debug.Log("Day one dialogue started");
    }

    public void StartDay1() {
        _gameController.NewCustomer();
    }
}
