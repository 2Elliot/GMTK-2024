using UnityEngine;

namespace InputHandler {
  public class InputReader : MonoBehaviour {
    public static InputReader Instance { get; private set; }

    private void Awake() {
      if (Instance == null) {
        Instance = this;
      } else {
        Destroy(gameObject);
        return;
      }

      InputActions = new PlayerInputActions();
      InputActions.Enable();
    }

    private void OnDestroy() {
      if (Instance == this) {
        InputActions.Disable();
      }
    }

    public void SwitchActiveInput(string actionMapName) {
      switch (actionMapName) {
        case "Player":
          InputActions.UI.Disable();
          InputActions.Player.Enable();
          break;
        case "UI":
          InputActions.Player.Disable();
          InputActions.UI.Enable();
          break;
        default:
          Debug.LogWarning($"Can't find action map with name {actionMapName}");
          break;
      }
    }

    public PlayerInputActions InputActions { get; private set; }
  }
}