using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour {
    private ScaleController _scaleController;
    private PrinterController _printerController;
    private DialogueHandler _dialogueHandler;
    private CounterWeightManager _counterWeightManager;
    
    [SerializeField] private SpriteRenderer _customerImageRenderer;
    [SerializeField] private TextMeshProUGUI _customerName;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private List<Customer> _customers;
    private int _previousCustomerId = -1;
    
    public float startGuessTime;
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
        
        // StartCoroutine(CallAfterOneFrame(NewCustomer)); // Because some other stuff initializes in Start()
    }

    private IEnumerator CallAfterOneFrame(System.Action callback) {
        yield return null;

        callback?.Invoke();
    }

    public void SubmitGuess(int guess) {
        if (!CanSubmitPrinter) return;
        
        float score = 0;

        float deltaTime = (Time.time - startGuessTime);
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
        
        _dialogueHandler.PlayDialogue(success ? _currentCustomer.EndDialogueSuccess : _currentCustomer.EndDialogueFailure, true, callbackFunction: GetNewCustomerOrNewDay);

        CanSubmitPrinter = false;
    }

    private void GetNewCustomerOrNewDay() {
        return;
    }
    
    public void NewCustomer(Customer customer) {
        if (customer == null) return;
        
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
        _startGuessTime = Time.time;
        List<Item> items = _currentCustomer.Items;

        int itemIndex = Random.Range(0, items.Count);
        _currentItem = items[itemIndex];
        _currentItemWeight = _currentItem.Weight;
        
        _scaleController.SetNewItem(_currentItem);

        CanSubmitPrinter = true;
    }
}
