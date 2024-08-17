using UnityEngine;
using UnityEngine.InputSystem;

public class WeightController : ClickableSprite {

  [HideInInspector] public bool HasCounterweight;
  
  private Vector3 Offset;

  public int Index;
  private ScaleController _scaleController;

  [SerializeField] private Transform _connectionPoint;
  public Transform CounterWeightPoint;
  
  protected override void Start() {
    _scaleController = SingletonContainer.Instance.ScaleController;
      
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
  }

  public void ChangeWeight(float amount) {
    _scaleController._weights[Index].weight += amount;
  }
}