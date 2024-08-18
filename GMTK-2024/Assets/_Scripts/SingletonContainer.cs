using UnityEngine;

// Use SingletonContainer so hold singletons
public class SingletonContainer : MonoBehaviour
{
    public static SingletonContainer Instance { get; private set; }
    public GameController GameController { get; private set; }
    public ScaleController ScaleController { get; private set; }
    public PrinterController PrinterController { get; private set; }
    public DialogueHandler DialogueHandler { get; private set; }
    public CounterWeightManager CounterWeightManager { get; private set; }
    public DayManager DayManager { get; private set; }

    [SerializeField] private GameController _gameController;
    [SerializeField] private ScaleController _scaleController;
    [SerializeField] private PrinterController _printerController;
    [SerializeField] private DialogueHandler _dialogueHandler;
    [SerializeField] private CounterWeightManager _counterWeightManager;
    [SerializeField] private DayManager _dayManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        GameController = _gameController;
        ScaleController = _scaleController;
        PrinterController = _printerController;
        DialogueHandler = _dialogueHandler;
        CounterWeightManager = _counterWeightManager;
        DayManager = _dayManager;

        if ((GameController == null) || (ScaleController == null) || (PrinterController == null) ||
            (DialogueHandler == null) || (CounterWeightManager == null)) {
            Debug.LogError($"Unassigned Reference in {this}");
        }
    }
}