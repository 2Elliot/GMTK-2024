using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{
    [SerializeField] private float _transitionTime;
    private Animator _transition;

    private void Start()
    {
        _transition = gameObject.GetComponent<Animator>();
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
    
    public IEnumerator PlayTransition(MonoBehaviour comingFrom, string functionName)
    {
        _transition.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);
        
        var method = comingFrom.GetType().GetMethod(functionName);
        if (method != null)
        {
            method.Invoke(comingFrom, null);
        }
        else
        {
            Debug.LogWarning($"Method '{functionName}' not found on {comingFrom.GetType().Name}");
        }
    }

}
