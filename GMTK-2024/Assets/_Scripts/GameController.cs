using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {
    private ScaleController _scaleController;
    private PrinterController _printerController;
    private DialogueHandler _dialogueHandler;
    private CounterWeightManager _counterWeightManager;
    private DayController _dayController;
    private MusicController _musicController;
    private FeedbackHolder _feedbackHolder;
    private DayCompleteManager _dayCompleteManager;
    private ShopManager _shopManager;

    // TODO: Change these and implement them @Elliot
    private int _score = 0;
    private int _money = 0;

    public bool SpendMoney(int amount) {
        if (CanAfford(amount)) {
            _money -= amount;
            return true;
        }
        return false;
    }

    public bool CanAfford(int amount) {
        if (amount <= _money) {
            return true;
        }
        return false;
    }
    
    [SerializeField] private SpriteRenderer _customerImageRenderer;
    
    public float StartGuessTime;
    private int _currentItemWeight;

    private Customer _currentCustomer;
    private Item _currentItem;

    public bool CanSubmitPrinter;

    private void Start() {
        Debug.Log("Elliot make sure to implement money and score in GameController.");
        SingletonContainer instance = SingletonContainer.Instance;
        _scaleController = instance.ScaleController;
        _printerController = instance.PrinterController;
        _dialogueHandler = instance.DialogueHandler;
        _counterWeightManager = instance.CounterWeightManager;
        _dayController = instance.DayController;
        _musicController = instance.MusicController;
        _feedbackHolder = instance.FeedbackHolder;
        _dayCompleteManager = instance.DayCompleteManager;
        _shopManager = instance.ShopManager;

        _musicController.PlayMusic();
        
        GetNewCustomerOrNewDay();
    }

    private IEnumerator CallAfterOneFrame(System.Action callback) {
        yield return null;

        callback?.Invoke();
    }

    public void SubmitGuess(int guess) {
        if (!CanSubmitPrinter) return;
        
        float deltaTime = Time.time - StartGuessTime;
        float deltaGuess = Mathf.Abs(guess - _currentItemWeight);
        
        // Money calculation
        int newMoney = 50;
        newMoney -= Mathf.RoundToInt(6 * deltaGuess);
        float clampedTime10 = Mathf.Clamp(deltaTime - 10, 0, 99);
        newMoney -= Mathf.RoundToInt(4 * clampedTime10);
        _money += newMoney;
        
        // Score calculation
        float newScore = 1000;
        newScore -= (100 * deltaGuess);
        float clampedTime4 = Mathf.Clamp(deltaTime - 4, 0, 99);
        newScore -= (50 * clampedTime4);
        if (deltaGuess == 0) {
            newScore *= 2f;
        }
        if (clampedTime4 == 0) {
            newScore *= 1.5f;
        }
        _score += Mathf.RoundToInt(newScore);

        // Success calculation
        bool success = true;
        if (deltaTime > 16f) {
            success = false;
        }
        if (deltaGuess > 3) {
            success = false;
        }
        
        Reset();
        
        
        _dialogueHandler.PlayDialogue(success ? _currentCustomer.EndDialogueSuccess : _currentCustomer.EndDialogueFailure, true, dialogueEndCallback: WaitForNextCustomer);

        CanSubmitPrinter = false;
    }

    private void WaitForNextCustomer() {
        float timeToWait = Random.Range(0.5f, 1.5f);
        CallFunctionsWithDelay(GetNewCustomerOrNewDay, timeToWait);
    }
    
    public void GetNewCustomerOrNewDay() {
        Customer customer = _dayController.FetchNewCustomer();
        if (customer == null) {
            if (_dayController._currentDayIndex != 0) { // day -1 needs to be finished to init everything probably
                OnDayEnd();
            }
            else {
                GetNewCustomerOrNewDay();
                return;
            }
        }

        _currentCustomer = customer;

        // _feedbackHolder.CustomerOut.PlayFeedbacks();
        CallFunctionsWithDelay(ChooseNewCustomer, 0.1f);
    }

    private void OnDayEnd() {
        _dayCompleteManager.SubscribeToDayComplete(OnDayEndQuit, OnDayEndContinueToStore);
        _dayCompleteManager.ShowDayComplete(_score, _money);
    }
    public void OnDayEndQuit() {
        Debug.LogWarning("Implement return to main menu here.");
    }
    public void OnDayEndContinueToStore() {
        _dayCompleteManager.HideDayComplete();
        _shopManager.ShowShop();
    }
    
    private void Reset() {
        _counterWeightManager.Reset();
        _scaleController.ResetScale();
        _printerController.Reset();
    }

    private void ChooseNewCustomer() {
        if (_currentCustomer.Image != null) _customerImageRenderer.sprite = _currentCustomer.Image;
        _feedbackHolder.CustomerIn.PlayFeedbacks();
        _dialogueHandler.PlayDialogue(_currentCustomer.StartDialogue, true, ChooseNewItem);
    }

    private void ChooseNewItem() {
        _counterWeightManager.Reset();
        
        StartGuessTime = Time.time;
        List<Item> items = _currentCustomer.Items;

        if (items.Count == 0) {
            Debug.Log($"Customer {_currentCustomer.name} doesn't have any items. Skipping to the next customer.");
            Reset();

            CallFunctionsWithDelay(GetNewCustomerOrNewDay, 1f);
            return;
        }

        int itemIndex = Random.Range(0, items.Count);
        _currentItem = items[itemIndex];
        _currentItemWeight = _currentItem.Weight;
        
        _scaleController.SetNewItem(_currentItem);

        CanSubmitPrinter = true;
    }
    
    void CallFunctionsWithDelay(Action function, float delay)
    {
        StartCoroutine(CallFunctionsWithDelayCoroutine(function, delay));
    }
    
    private IEnumerator CallFunctionsWithDelayCoroutine(Action function, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        function?.Invoke();
    }
}
