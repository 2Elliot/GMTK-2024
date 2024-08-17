using UnityEngine;

// Use SingletonContainer so hold singletons
public class SingletonContainer : MonoBehaviour
{
    public static SingletonContainer Instance { get; private set; }
    public GameController GameController { get; private set; }
    public ScaleController ScaleController { get; private set; }
    public RegisterController RegisterController { get; private set; }
    public DialogueHandler DialogueHandler { get; private set; }
    public CounterWeightManager CounterWeightManager { get; private set; }

    [SerializeField] private GameController _gameController;
    [SerializeField] private ScaleController _scaleController;
    [SerializeField] private RegisterController _registerController;
    [SerializeField] private DialogueHandler _dialogueHandler;
    [SerializeField] private CounterWeightManager _counterWeightManager;

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
        RegisterController = _registerController;
        DialogueHandler = _dialogueHandler;
        CounterWeightManager = _counterWeightManager;

        if ((GameController == null) || (ScaleController == null) || (RegisterController == null) ||
            (DialogueHandler == null) || (CounterWeightManager == null)) {
            Debug.LogError($"Unassigned Reference in {this}");
        }
    }
}