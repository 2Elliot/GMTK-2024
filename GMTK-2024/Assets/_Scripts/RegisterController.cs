using TMPro;
using UnityEngine;

public class RegisterController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _text;

    private GameController _customerController;
    
    private int _guess = 0;

    private void Start() {
        _customerController = SingletonContainer.Instance.GameController;
    }

    public void Add(int amount) {
        _guess += amount;
        _guess = Mathf.Clamp(_guess, 0, 99);
        _text.text = _guess.ToString();
    }

    public void Reset() {
        _guess = 0;
        _text.text = _guess.ToString();
    }

    public void Submit() {
        _customerController.NewCustomer();
    }
}
