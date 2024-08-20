using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayCompleteManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private ValueLerpAnimator _scoreAnimator;
    [SerializeField] private ValueLerpAnimator _moneyAnimator;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _quitButton;
    
    public delegate void OnQuitButtonClicked();
    public delegate void OnContinueButtonClicked();

    private void Start() {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
    public void SubscribeToDayComplete(OnQuitButtonClicked quitCallback, OnContinueButtonClicked continueCallback) {
        _quitButton.onClick.AddListener(() => {
            quitCallback?.Invoke();
        });
        _continueButton.onClick.AddListener(() => {
            continueCallback?.Invoke();
        });
    }
    private void OnDisable() {
        _continueButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }

    public void ShowDayComplete(int score, int money) {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _scoreAnimator.AnimateValue(0, score);
        _moneyAnimator.AnimateValue(0, money);
    }
    
    public void HideDayComplete() {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}
