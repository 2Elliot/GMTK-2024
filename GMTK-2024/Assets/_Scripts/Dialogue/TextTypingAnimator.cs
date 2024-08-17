using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTypingAnimator : MonoBehaviour
{
    public TMPro.TextMeshProUGUI _textMeshPro;
    [TextArea(3, 10)] public string _text;
    public float _typingSpeed = 0.1f;
    public AudioClip _typingSound;
    private string _displayedText;
    private bool _typing;

    private void OnValidate() {
        if (_textMeshPro != null) {
            _textMeshPro.text = _text;
        }
    }

    public void StartTyping() {
        if (_typing) return;
        _displayedText = "";
        StartCoroutine(TypeText());
    }
    public void ResetText() {
        StopCoroutine(TypeText());
        _displayedText = "";
        _textMeshPro.text = _displayedText;
    }

    public IEnumerator TypeText() {
        _typing = true;
        _displayedText = "";
        _textMeshPro.text = _displayedText;
        for (int i = 0; i < _text.Length; i++) {
            _displayedText += _text[i];
            _textMeshPro.text = _displayedText;
            yield return new WaitForSeconds(_typingSpeed);
        }
        
        _typing = false;
    }
}
