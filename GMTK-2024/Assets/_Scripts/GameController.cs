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
    
    [SerializeField] private SpriteRenderer _customerImageRenderer;
    [SerializeField] private TextMeshProUGUI _customerName;
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    public float StartGuessTime;
    private int _currentItemWeight;

    private Customer _currentCustomer;
    private Item _currentItem;

    public bool CanSubmitPrinter;

    private void Start() {
        SingletonContainer instance = SingletonContainer.Instance;
        _scaleController = instance.ScaleController;
        _printerController = instance.PrinterController;
        _dialogueHandler = instance.DialogueHandler;
        _counterWeightManager = instance.CounterWeightManager;
        _dayController = instance.DayController;

        // StartCoroutine(CallAfterOneFrame(NewCustomer)); // Because some other stuff initializes in Start()
    }

    private IEnumerator CallAfterOneFrame(System.Action callback) {
        yield return null;

        callback?.Invoke();
    }

    public void SubmitGuess(int guess) {
        if (!CanSubmitPrinter) return;
        
        float score = 0;

        float deltaTime = (Time.time - StartGuessTime);
        float deltaGuess = Mathf.Abs(guess - _currentItemWeight);

        const float maxTimeBeforePenalty = 8f;
        // Score calculations
        if (deltaTime > maxTimeBeforePenalty) {
            score += (deltaTime - maxTimeBeforePenalty);
        }
        score += deltaGuess;

        bool success;
        string scoreText;
        if (score == 0) {
            scoreText = "S+";
            success = true;
        } else if (score <= 1.5) {
            scoreText = "S";
            success = true;
        } else if (score <= 3) {
            scoreText = "A";
            success = true;
        } else if (score <= 4.5) {
            scoreText = "B";
            success = true;
        } else if (score <= 6.5) {
            scoreText = "C";
            success = false;
        } else {
            scoreText = "F";
            success = false;
        }

        _scoreText.text = scoreText;
        
        Reset();
        
        _dialogueHandler.PlayDialogue(success ? _currentCustomer.EndDialogueSuccess : _currentCustomer.EndDialogueFailure, true, callbackFunction: WaitForNextCustomer);

        CanSubmitPrinter = false;
    }

    private void WaitForNextCustomer() {
        float timeToWait = Random.Range(0.5f, 1.5f);
        CallFunctionsWithDelay(GetNewCustomerOrNewDay, timeToWait);
    }
    
    public void GetNewCustomerOrNewDay() {
        Customer customer = _dayController.FetchNewCustomer();
        if (customer == null) {
            Debug.Log("New day, put code here.");
            GetNewCustomerOrNewDay();
            return;
        }
        ChooseNewCustomer(customer);
    }

    private void Reset() {
        _scaleController.ResetScale();
        _printerController.Reset();
        _counterWeightManager.Reset();
    }

    private void ChooseNewCustomer(Customer customer) {
        _currentCustomer = customer;

        _customerImageRenderer.sprite = _currentCustomer.Image;
        _customerName.text = _currentCustomer.Name;
        _dialogueHandler.PlayDialogue(_currentCustomer.StartDialogue, true, ChooseNewItem);
    }

    private void ChooseNewItem() {
        StartGuessTime = Time.time;
        List<Item> items = _currentCustomer.Items;

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
