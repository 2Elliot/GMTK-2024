using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPointController : MonoBehaviour {
    [SerializeField] private Vector3 _pos;

    public void Reset() {
        transform.localPosition = _pos;
    }
}
