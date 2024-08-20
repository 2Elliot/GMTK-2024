using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenButtonDefaultValue : MonoBehaviour
{
  private void Start() {
    if (Screen.fullScreen) {
      GetComponent<UnityEngine.UI.Toggle>().isOn = true;
    } else {
      GetComponent<UnityEngine.UI.Toggle>().isOn = false;
    }
  }
}
