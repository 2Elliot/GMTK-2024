using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueActiveGroup : MonoBehaviour
{
    [Header("Assign this script to UniqueActiveIndividuals")]
    [SerializeField] private List<UniqueActiveIndividual> _objects;
    [SerializeField] private int _activeIndex = 0;

    private void Start()
    {
        if (_objects.Count > 0) 
        {
            ResetObjects();
        }
    }

    public void AttachToGroup(UniqueActiveIndividual individual)
    {
        _objects.Add(individual);
    }

    public void OnPointerEnterFor(UniqueActiveIndividual individual)
    {
        if (!_objects.Contains(individual)) {
            Debug.LogError("UniqueActiveGroup: Individual calling OnPointerEnter not found in group");
            return;
        }
        _activeIndex = _objects.IndexOf(individual);
        ResetObjects();
        _objects[_activeIndex].ActivateObjects();
    }

    public void ResetObjects()
    {
        foreach (var obj in _objects)
        {
            obj.DisableObjects();
        }
    }
}
