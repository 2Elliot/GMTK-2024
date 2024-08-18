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
        float score = 0;

        float deltaTime = (Time.time - startGuessTime);
        float deltaGuess = Mathf.Abs(guess - _currentItemWeight);

        const float maxTimeBeforePenalty = 8f;
        // Score calculations
        if (deltaTime > maxTimeBeforePenalty) {
            score += (deltaTime - maxTimeBeforePenalty);
        }
        score += deltaGuess;

        string scoreText;
        if (score == 0) {
            scoreText = "S+";
        } else if (score <= 1.5) {
            scoreText = "S";
        } else if (score <= 3) {
            scoreText = "A";
        } else if (score <= 4.5) {
            scoreText = "B";
        } else if (score <= 6.5) {
            scoreText = "C";
        } else {
            scoreText = "F";
        }

        _scoreText.text = scoreText;
        
        NewCustomer();
    }
    
    public void NewCustomer() {
        Reset();
        
        ChooseNewCustomer();

        ChooseNewItem();

        _scaleController.ResetScale(_currentItem);

        startGuessTime = Time.time;
    }

    private void Reset() {
        _printerController.Reset();

        _counterWeightManager.Reset();
    }

    private void ChooseNewCustomer() {
        int currentCustomerID = -1;

        currentCustomerID = Random.Range(0, _customers.Count);
        while (currentCustomerID == _previousCustomerId) {
            currentCustomerID = Random.Range(0, _customers.Count);
        }
        _previousCustomerId = currentCustomerID;

        _currentCustomer = _customers[currentCustomerID];

        _customerImageRenderer.sprite = _currentCustomer.Image;
        _customerName.text = _currentCustomer.Name;
        _dialogueHandler.PlayDialogue(_currentCustomer.Dialogue, true);
    }

    private void ChooseNewItem() {
        List<Item> items = _currentCustomer.Items;

        int itemIndex = Random.Range(0, items.Count);
        _currentItem = items[itemIndex];
        _currentItemWeight = _currentItem.Weight;
    }
}
