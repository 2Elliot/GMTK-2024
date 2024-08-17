using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CounterWeightManager : MonoBehaviour {
    [SerializeField] private List<GameObject> _prefabs;
    public bool[] Unlocks;
    public List<Vector3> OriginalPositions;

    private List<GameObject> _counterweights = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start() {
        Reset();
    }

    public void AddCounterweight(GameObject weight, int index) {
        GameObject obj = Instantiate(weight, OriginalPositions[index], Quaternion.identity);
        _counterweights.Add(obj);
        obj.transform.GetComponent<CounterWeight>().Index = index;
    }

    public void Reset() {
        if (_counterweights.Count != 0) {
            foreach (GameObject g in _counterweights) {
                Destroy(g);
            }
        }

        for (int i = 0; i < Unlocks.Length; i++) {
            if (Unlocks[i]) AddCounterweight(_prefabs[i], i);
        }
    }
}
