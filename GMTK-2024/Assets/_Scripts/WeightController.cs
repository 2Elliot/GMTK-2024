using UnityEngine;
using UnityEngine.InputSystem;

public class WeightController : ClickableSprite {

  [HideInInspector] public bool HasCounterweight;
  
  private Vector3 Offset;

  public int Index;
  private ScaleController _scaleController;

  public SpriteRenderer _otherWeightSprite;

  [SerializeField] private Transform _connectionPoint;
  public Transform CounterWeightPoint;

  private SFXController _sfxController;
  private float _previousXPosition;

  public bool CanHoldObject = true;
  
  protected override void Start() {
    _scaleController = SingletonContainer.Instance.ScaleController;
    _sfxController = GetComponent<SFXController>();
    _previousXPosition = transform.localPosition.x;

    CanHoldObject = true;
      
    base.Start();
    Offset = transform.position - _connectionPoint.position;
  }

  public void SetPosition(Vector3 position) {
    transform.position = position + Offset;
  }

  protected override void OnSpriteHeld() {
    Vector2 mousePositionScreen = Mouse.current.position.ReadValue();
    Vector3 mousePositionWorld = MainCamera.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, 0));
        
    _scaleController.SetWeightXPosition(Index, mousePositionWorld);
    if (Mathf.Abs(_previousXPosition - transform.localPosition.x) > 1f) {
      _previousXPosition = transform.localPosition.x;
      _sfxController.PlaySound();
    }
    _previousXPosition = transform.localPosition.x;
  }

  public void ChangeWeight(float amount) {
    _scaleController._weights[Index].weight += amount;
    
    // Yup its bad
    if (amount > 0) {
      Debug.Log("Added Object");
      CanHoldObject = false;
    }
    else {
      Debug.Log("Removed Object");
      CanHoldObject = true;
    }
  }
}