using UnityEngine;
using UnityEngine.InputSystem;
using MoreMountains.Feedbacks;

public class CounterWeight : ClickableSprite {
    private CounterWeightManager _counterWeightManager;

    [SerializeField] private LayerMask _counterweightLayer;
    
    [SerializeField] private Transform _offsetTransform;
    private Vector3 _offset;
    
    [HideInInspector] public int Index;

    [SerializeField] private float weight;

    private MMFeedbacks _screenShake;

    private bool _inWeight;
    private WeightController _weightController;
    
    // Start is called before the first frame update
    protected override void Start() {
        _counterWeightManager = SingletonContainer.Instance.CounterWeightManager;
        _screenShake = SingletonContainer.Instance.FeedbackHolder.ScreenShake;
        
        base.Start();
        _offset = transform.position - _offsetTransform.position;
    }

    protected override void OnSpriteClicked() {
        if (_inWeight) {
            _weightController.ChangeWeight(-weight);
            _inWeight = false;
            _weightController = null;
        }
    }

    protected override void OnSpriteHeld() {
        Vector3 mousePositionWorld = MouseToWorld();
        mousePositionWorld.z = -5f;
        
        transform.position = mousePositionWorld + _offset;
    }

    protected override void OnSpriteReleased() {
        _screenShake.PlayFeedbacks();
        
        Vector3 origin = MouseToWorld();
        Vector3 direction = new Vector3(0, 0, 1);
        float distance = 10f;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, _counterweightLayer);

        if (hit.collider == null) {
            transform.position = _counterWeightManager.OriginalPositions[Index];
            return;
        }
        
        if (hit.transform.CompareTag("Weight")) {
            if (hit.transform.GetComponent<WeightController>().Index == 1) {
                // Really bad way but works
                _weightController = hit.transform.GetComponent<WeightController>();
            }
            else {
                transform.position = _counterWeightManager.OriginalPositions[Index];
                return;
            }
        } else {
            transform.position = _counterWeightManager.OriginalPositions[Index];
            return;
        }

        _weightController.ChangeWeight(weight);
        _inWeight = true;
    }

    protected override void Update() {
        base.Update();

        if (_inWeight) {
            transform.position = _weightController.CounterWeightPoint.position + _offset;
        }
    }

    private Vector3 MouseToWorld() {
        Vector2 mousePositionScreen = Mouse.current.position.ReadValue();
        Vector3 mousePositionWorld = MainCamera.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, 0));

        return mousePositionWorld;
    }
}
