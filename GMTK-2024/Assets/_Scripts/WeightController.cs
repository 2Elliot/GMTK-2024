using UnityEngine;
using UnityEngine.InputSystem;

public class WeightController : IClickableSprite {

  private Vector3 Offset;

  public int Index;
  public ScaleController ScaleController;

  [SerializeField] private Transform _connectionPoint;
  
  protected override void Start() {
    base.Start();
    Offset = transform.position - _connectionPoint.position;
  }

  public void SetPosition(Vector3 position) {
    transform.position = position + Offset;
  }

  protected override void OnSpriteHeld() {
    Vector2 mousePositionScreen = Mouse.current.position.ReadValue();
    Vector3 mousePositionWorld = MainCamera.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, 0));
        
    ScaleController.SetWeightXPosition(Index, mousePositionWorld);
  }
}