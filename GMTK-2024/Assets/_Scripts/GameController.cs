using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
    private ScaleController _scaleController;
    private PrinterController _printerController;
    private DialogueHandler _dialogueHandler;
    private CounterWeightManager _counterWeightManager;
    private DayManager _dayManager;
    
    [SerializeField] private SpriteRenderer _customerImageRenderer;
    [SerializeField] private TextMeshProUGUI _customerName;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private List<Customer> _customers;
    private int _previousCustomerId = -1;

    private float _startGuessTime;
    private int _currentItemWeight;

    private Customer _currentCustomer;
    private Item _currentItem;

    private void Start() {
        SingletonContainer instance = SingletonContainer.Instance;
        _scaleController = instance.ScaleController;
        _printerController = instance.PrinterController;
        _dialogueHandler = instance.DialogueHandler;
        _counterWeightManager = instance.CounterWeightManager;
        _dayManager = instance.DayManager;
        
        StartCoroutine(CallAfterOneFrame(_dayManager.StartGame)); // Because some other stuff initializes in Start()
    }

    private IEnumerator CallAfterOneFrame(System.Action callback) {
        yield return null;

        callback?.Invoke();
    }

    public void SubmitGuess(int guess) {
        float score = 0;

        float deltaTime = (Time.time - _startGuessTime);
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
        
        _dayManager.NextCustomer();
    }
    
    public void NewCustomer(Customer customer) {
        Reset();
        
        ChooseNewCustomer(customer);

        ChooseNewItem();

        _scaleController.ResetScale(_currentItem);

        _startGuessTime = Time.time;
    }

    private void Reset() {
        _printerController.Reset();

        _counterWeightManager.Reset();
    }

    private void ChooseNewCustomer(Customer customer)
    {
        _currentCustomer = customer;

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
