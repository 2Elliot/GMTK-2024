using UnityEngine;

// Use SingletonContainer so hold singletons
public class SingletonContainer : MonoBehaviour
{
    public static SingletonContainer Instance { get; private set; }
    public GameController GameController { get; private set; }
    public DayController DayController { get; private set; }
    public ScaleController ScaleController { get; private set; }
    public PrinterController PrinterController { get; private set; }
    public DialogueHandler DialogueHandler { get; private set; }
    public CounterWeightManager CounterWeightManager { get; private set; }
    public FeedbackHolder FeedbackHolder { get; private set; }
    public MusicController MusicController { get; private set; }
    public PauseController PauseController { get; private set; }

    [SerializeField] private GameController _gameController;
    [SerializeField] private DayController _dayController;
    [SerializeField] private ScaleController _scaleController;
    [SerializeField] private PrinterController _printerController;
    [SerializeField] private DialogueHandler _dialogueHandler;
    [SerializeField] private CounterWeightManager _counterWeightManager;
    [SerializeField] private FeedbackHolder _feedbackHolder;
    [SerializeField] private MusicController _musicController;
    [SerializeField] private PauseController _pauseController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        GameController = _gameController;
        DayController = _dayController;
        ScaleController = _scaleController;
        PrinterController = _printerController;
        DialogueHandler = _dialogueHandler;
        CounterWeightManager = _counterWeightManager;
        FeedbackHolder = _feedbackHolder;
        MusicController = _musicController;
        PauseController = _pauseController;

        if ((GameController == null) || (DayController == null) || (ScaleController == null) || (PrinterController == null) ||
            (DialogueHandler == null) || (CounterWeightManager == null) || (FeedbackHolder == null) || (MusicController == null) || (PauseController == null)) {
            Debug.LogError($"Unassigned Reference in {this}");
        }
    }
}