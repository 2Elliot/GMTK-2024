using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class CounterWeightManager : MonoBehaviour {
    [SerializeField] private List<GameObject> _prefabs;
    public bool[] Unlocks;
    public List<GameObject> _originalObjects;
    public List<Vector3> OriginalPositions;
    
    public List<GameObject> _counterweights = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < _originalObjects.Count; i++) {
            OriginalPositions.Add(_originalObjects[i].transform.position);
            Destroy(_originalObjects[i]);
        }
        
        Reset();
    }

    public void AddCounterweight(GameObject weight, int index) {
        GameObject obj = Instantiate(weight, OriginalPositions[index], Quaternion.identity);
        _counterweights.Add(obj);
        obj.transform.GetComponent<CounterWeight>().Index = index;
    }

    public void Reset() {
        Debug.Log("Reset");
        foreach (GameObject g in _counterweights) {
            Destroy(g);
        }

        _counterweights.Clear();

        for (int i = 0; i < Unlocks.Length; i++) {
            if (Unlocks[i]) AddCounterweight(_prefabs[i], i);
        }
    }
}
