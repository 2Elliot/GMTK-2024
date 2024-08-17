using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour {

    [SerializeField] private Transform _bar;

    [System.Serializable]
    public struct Weight {
        public Transform connectionPoint;
        public WeightController weightObject;
        public float weight;
        [Range(-0.5f, 0.5f)] public float xPos;
    }

    [SerializeField] private Weight[] _weights;
    
    [SerializeField, Range(-30, 30)] private float _currentRotation = 0;
    [SerializeField] private float _rotationMultiplier = 20f;
    
    public float CalculateRotation(Weight weight1, Weight weight2) {
        // Calculate the moments of each weight around the center (moment = weight * distance)
        float moment1 = weight1.weight * weight1.xPos;
        float moment2 = weight2.weight * weight2.xPos;

        // Calculate the net moment
        float netMoment = moment1 + moment2;

        // Convert net moment to an angle
        float angle = netMoment * _rotationMultiplier;
        angle = Mathf.Clamp(angle, -30f, 30f);

        return -angle; // Negative because Unity's weird
    }
    
    private void Update() {

        foreach (Weight currWeight in _weights) {
            Vector3 connectionPointLocalPosition = currWeight.connectionPoint.localPosition;
            currWeight.connectionPoint.localPosition = new Vector3(currWeight.xPos, connectionPointLocalPosition.y,
                connectionPointLocalPosition.z);

            currWeight.weightObject.SetPosition(currWeight.connectionPoint.position);
        }

        _currentRotation = CalculateRotation(_weights[0], _weights[1]);
        
        _bar.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
            _currentRotation);
    }





}
