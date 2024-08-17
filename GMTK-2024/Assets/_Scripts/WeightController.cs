using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightController : MonoBehaviour {
    [SerializeField] private Transform _connectionPoint;

    private Vector3 offset;
    
    private void Start() {
        offset = transform.position - _connectionPoint.position;
    }

    public void SetPosition(Vector3 position) {
        transform.position = position + offset;
    }
}
