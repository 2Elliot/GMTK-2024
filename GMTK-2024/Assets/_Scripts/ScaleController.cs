using UnityEngine;

public class ScaleController : MonoBehaviour {

    [SerializeField] private Transform _bar;

    [SerializeField] private GameObject _chainPrefab;

    [SerializeField] private Transform[] _connectionPoints;

    private bool _initiated;
    
    [System.Serializable]
    public struct Weight {
        public GameObject weightObject;
        public Transform connectionPoint;
        public WeightController weightScript;
        public float weight;
        [Range(1f, 4.5f)] public float xPos;
        [Range(-1, 1)] public int direction; // -1 for left, 1 for right
    }

    [SerializeField] public Weight[] _weights;
    
    [SerializeField, Range(-30, 30)] private float _currentRotation = 0;
    [SerializeField] private float _rotationMultiplier = 5f;
    
    private float CalculateRotation(Weight weight1, Weight weight2) {
        float moment1 = weight1.weight * weight1.xPos * weight1.direction;
        float moment2 = weight2.weight * weight2.xPos * weight2.direction;

        float netMoment = moment1 + moment2;

        float angle = netMoment * _rotationMultiplier;
        angle = Mathf.Clamp(angle, -30f, 30f);

        return -angle; // Negative because Unity's weird
    }

    private void Start() {
        _weights = new Weight[2];
    }

    private void Update() {
        if (!_initiated) return;
        
        foreach (Weight currWeight in _weights) {
            Vector3 connectionPointLocalPosition = currWeight.connectionPoint.localPosition;
            currWeight.connectionPoint.localPosition = new Vector3(currWeight.xPos * currWeight.direction, connectionPointLocalPosition.y,
                connectionPointLocalPosition.z);

            currWeight.weightScript.SetPosition(currWeight.connectionPoint.position);
        }

        _currentRotation = CalculateRotation(_weights[0], _weights[1]);
        
        _bar.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
            _currentRotation);
    }

    public void SetWeightXPosition(int index, Vector2 position) {
        Vector2 snappedPosition = SnapPointToLine(position);
        Vector3 newPosition = transform.InverseTransformPoint(snappedPosition);
        int direction = _weights[index].direction;
        float xPos = Mathf.Clamp(direction * newPosition.x, 1f, 4.5f);
        _weights[index].xPos = ConvertToClosest(xPos);
    }
    
    private static float ConvertToClosest(float input)
    {
        float[] targets = { 1.5f, 2.75f, 4f };
        
        float closest = targets[0];
        float minDifference = Mathf.Abs(input - closest);

        foreach (float target in targets)
        {
            float difference = Mathf.Abs(input - target);
            if (difference < minDifference)
            {
                closest = target;
                minDifference = difference;
            }
        }

        return closest;
    }

    private Vector2 SnapPointToLine(Vector2 point) {
        Vector2 lineDirection = AngleToDirection(_currentRotation);
        lineDirection.Normalize();

        Vector2 pointToLineStart = point - (Vector2)transform.position;
        float projectionLength = Vector2.Dot(pointToLineStart, lineDirection);
        Vector2 projection = (Vector2)transform.position + projectionLength * lineDirection;
        
        return new Vector2(point.x, projection.y);
    }

    private static Vector2 AngleToDirection(float angle) {
        float radians = Mathf.Deg2Rad * angle;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    public void ResetScale(Item item) {
        if (_initiated) {
            Destroy(_weights[0].weightObject);
            Destroy(_weights[1].weightObject);
        }

        _currentRotation = 0;
        transform.rotation = Quaternion.identity;

        _connectionPoints[0].GetComponent<ConnectionPointController>().Reset();
        _connectionPoints[1].GetComponent<ConnectionPointController>().Reset();

        // Customer's item
        GameObject obj = InstantiateItem(item, 0);
        Weight weight0 = new Weight {
            weightObject = obj,
            weightScript = obj.GetComponent<WeightController>(),
            connectionPoint = _connectionPoints[0],
            weight = item.Weight,
            xPos = 4,
            direction = -1
        };

        // Your item
        GameObject obj2 = InstantiateItem(1);
        Weight weight1 = new Weight {
            weightObject = obj2,
            weightScript = obj2.GetComponent<WeightController>(),
            connectionPoint = _connectionPoints[1],
            weight = 0, // Temporary value I hard coded
            xPos = 4,
            direction = 1
        };

        _weights[0] = weight0;
        _weights[1] = weight1;

        _initiated = true;

    }

    private GameObject InstantiateItem(Item item, int index) {
        GameObject newItem = Instantiate(_chainPrefab);
        newItem.GetComponent<WeightController>().Index = index;

        newItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item.Image;
        return newItem;
    }
    
    private GameObject InstantiateItem(int index) {
        GameObject newItem = Instantiate(_chainPrefab);
        newItem.GetComponent<WeightController>().Index = index;
        return newItem;
    }
}
