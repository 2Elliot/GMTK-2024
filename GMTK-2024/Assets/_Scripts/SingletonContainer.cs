using UnityEngine;

// Use SingletonContainer so hold singletons
public class SingletonContainer : MonoBehaviour
{
    public static SingletonContainer Instance { get; private set; }
    public GameController GameController { get; private set; }
    public ScaleController ScaleController { get; private set; }
    public RegisterController RegisterController { get; private set; }
    public DialogueHandler DialogueHandler { get; private set; }

    [SerializeField] private GameController _gameController;
    [SerializeField] private ScaleController _scaleController;
    [SerializeField] private RegisterController _registerController;
    [SerializeField] private DialogueHandler _dialogueHandler;


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

        if ((GameController == null) || (ScaleController == null) || (RegisterController == null) ||
            (DialogueHandler == null)) {
            Debug.LogError($"Unassigned Reference in {this}");
        }
    }
}