using UnityEngine;

// Use SingletonContainer so hold singletons
public class SingletonContainer : MonoBehaviour
{
    public static SingletonContainer Instance { get; private set; }
    public CustomerController CustomerController { get; private set; }
    public RegisterController RegisterController { get; private set; }

    [SerializeField] private CustomerController _customerController;
    [SerializeField] private RegisterController _registerController;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        CustomerController = _customerController;
        RegisterController = _registerController;
    }
}