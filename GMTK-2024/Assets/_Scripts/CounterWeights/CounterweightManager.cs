using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterWeightManager : MonoBehaviour
{
    public List<CounterWeight> counterweights;
    public List<Vector3> counterweightsTransform; // originial transform of counterweight
    [SerializeField] public ScaleController scaleController;
    public GameObject scale;


    public bool isObjectClicked;

    public float chainSnapRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            counterweights.Add(transform.GetChild(i).GetComponent<CounterWeight>());
            counterweightsTransform.Add(transform.GetChild(i).transform.position);
            counterweights[i].manager = this;
            counterweights[i].index = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(scaleController._weights.Length > 0)
            scale = scaleController._weights[1].weightObject;
        else
            scale = null;
    }
}
