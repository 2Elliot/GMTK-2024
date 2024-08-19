using UnityEngine;
using UnityEngine.InputSystem;
using MoreMountains.Feedbacks;
using TMPro;

public class CounterWeight : ClickableSprite {
    private CounterWeightManager _counterWeightManager;

    [SerializeField] private LayerMask _counterweightLayer;
    
    private Vector3 _offset;
    
    [HideInInspector] public int Index;

    [SerializeField] private float weight;

    private MMFeedbacks _screenShake;

    private WeightController _weightController;

    private SFXController _sfxController;

    private Sprite _sprite;
    [SerializeField] private Sprite _outlinedSprite;

    [SerializeField] private Sprite _32Sprite;
    [SerializeField] private Sprite _32SpriteOutlined;
    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;

    private GameObject _canvas;
    
    public int ScreenShakeSize; // 0 = small, 1 = medium, 2 = big

    private bool _inPlatform;
    
    // Start is called before the first frame update
    private void Start() {
        _counterWeightManager = SingletonContainer.Instance.CounterWeightManager;

        _renderer = GetComponent<SpriteRenderer>();
        _sprite = _renderer.sprite;

        _collider = GetComponent<BoxCollider2D>();

        _canvas = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        _canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = weight.ToString(); // This hard coding is just cause I'm lazy
        
        _canvas.SetActive(false);

        switch (ScreenShakeSize) {
            case 0:
                _screenShake = SingletonContainer.Instance.FeedbackHolder.SmallScreenShake;
                break;
            case 1:
                _screenShake = SingletonContainer.Instance.FeedbackHolder.MediumScreenShake;
                break;
            case 2:
                _screenShake = SingletonContainer.Instance.FeedbackHolder.BigScreenShake;
                break;
            case 3:
                _screenShake = SingletonContainer.Instance.FeedbackHolder.MassiveScreenShake;
                break;
            default:
                Debug.LogWarning("Bad value");
                break;
        }

        _sfxController = GetComponent<SFXController>();
        _offset = new Vector3(0, (4.0625f - 3.6875f), 0);
    }

    protected override void OnSpriteClicked() {
        _canvas.SetActive(false);
        if (_inPlatform) {
            _weightController.ChangeWeight(-weight);
            _inPlatform = false;
            _weightController = null;
        }
    }

    protected override void OnSpriteHeld() {
        Vector3 mousePositionWorld = MouseToWorld();
        mousePositionWorld.z = -5f;
        
        transform.position = mousePositionWorld + _offset;

        _counterWeightManager.SomethingHeld = true;
    }

    protected override void OnSpriteReleased() {
        _counterWeightManager.SomethingHeld = false;
        
        _screenShake.PlayFeedbacks();
        GetComponent<SFXController>().PlaySound();
        
        Vector3 origin = MouseToWorld();
        Vector3 direction = new Vector3(0, 0, 1);
        float distance = 10f;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, _counterweightLayer);

        if (hit.collider == null) {
            transform.position = _counterWeightManager.OriginalPositions[Index];
            
            transform.localScale = Vector3.one;
            _renderer.sprite = _sprite;
            return;
        }
        
        if (hit.transform.CompareTag("Weight")) {
            if (!hit.transform.GetComponent<WeightController>().CanHoldObject) {
                transform.position = _counterWeightManager.OriginalPositions[Index];
                
                transform.localScale = Vector3.one;
                _renderer.sprite = _sprite;
                _collider.size = Vector2.one;
                return;
            }

            if (hit.transform.GetComponent<WeightController>().Index == 1) {
                // Really bad way but works
                _weightController = hit.transform.GetComponent<WeightController>();
            }
            else {
                transform.position = _counterWeightManager.OriginalPositions[Index];
                
                transform.localScale = Vector3.one;
                
                _renderer.sprite = _sprite;
                _collider.size = Vector2.one;
                return;
            }
        } else {
            transform.position = _counterWeightManager.OriginalPositions[Index];
            
            transform.localScale = Vector3.one;
            
            _renderer.sprite = _sprite;
            _collider.size = Vector2.one;
            return;
        }

        _weightController.ChangeWeight(weight);
        _inPlatform = true;

        _renderer.sprite = _32Sprite;
        _collider.size = Vector2.one * 2;
        
        Debug.Log("Change this line to change scaling of counterweight when on scale");
        transform.localScale = Vector3.one;
    }

    protected override void Update() {
        base.Update();

        if (_inPlatform && _weightController.CounterWeightPoint != null) {
            transform.position = _weightController.CounterWeightPoint.position + _offset;
        }
    }

    private Vector3 MouseToWorld() {
        Vector2 mousePositionScreen = Mouse.current.position.ReadValue();
        Vector3 mousePositionWorld = MainCamera.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, 0));

        return mousePositionWorld;
    }

    protected override void OnMouseHoverEnter() {
        _renderer.sprite = (_inPlatform ? _32SpriteOutlined : _outlinedSprite);
        if (!_inPlatform && !_counterWeightManager.SomethingHeld) _canvas.SetActive(true);
    }

    protected override void OnMouseHoverExit() {
        _renderer.sprite = (_inPlatform ? _32Sprite : _sprite);
        _canvas.SetActive(false);
    }
}
