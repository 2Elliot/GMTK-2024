using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CurserController : MonoBehaviour
{
    private Camera _camera;
     
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Draw Rey
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = _camera.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos-transform.position, Color.blue);
    }
}
