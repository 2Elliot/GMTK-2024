using UnityEngine;

public class ScaleController : MonoBehaviour {

    [SerializeField] private Transform _bar;

    [System.Serializable]
    public struct Weight {
        public Transform connectionPoint;
        public WeightController weightObject;
        public float weight;
        [Range(1f, 4.5f)] public float xPos;
        [Range(-1, 1)] public int direction; // -1 for left, 1 for right
    }

    [SerializeField] private Weight[] _weights;
    
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
        for (int i = 0; i < _weights.Length; i++) {
            _weights[i].weightObject.Index = i;
            _weights[i].weightObject.scaleController = this;
        }
    }

    private void Update() {

        foreach (Weight currWeight in _weights) {
            Vector3 connectionPointLocalPosition = currWeight.connectionPoint.localPosition;
            currWeight.connectionPoint.localPosition = new Vector3(currWeight.xPos * currWeight.direction, connectionPointLocalPosition.y,
                connectionPointLocalPosition.z);

            currWeight.weightObject.SetPosition(currWeight.connectionPoint.position);
        }

        _currentRotation = CalculateRotation(_weights[0], _weights[1]);
        
        _bar.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
            _currentRotation);
    }

    public void SetWeightXPosition(int index, Vector2 position) {
        Vector2 snappedPosition = SnapPointToLine(position);
        Vector3 newPosition = transform.InverseTransformPoint(snappedPosition);
        int direction = _weights[index].direction;
        _weights[index].xPos = Mathf.Clamp(direction * newPosition.x, 1f, 4.5f); // jank af
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
}
