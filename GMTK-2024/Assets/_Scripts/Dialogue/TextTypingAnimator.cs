using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

public class TextTypingAnimator : MonoBehaviour
{
    public TMPro.TextMeshProUGUI _textMeshPro;
    private string _displayedText;
    public bool _isTyping;

    [SerializeField] private TextMeshProUGUI _skipText;
    
    Coroutine _coroutine;

    public void StartTyping(DialogueTurn turn, float durationPerCharacter) {
        if (_isTyping) {
            Debug.LogError("TextTypingAnimator: Already typing! Call ResetText() first!");
            return;
        }
        _displayedText = "";
        _coroutine = StartCoroutine(TypeText(turn._text, durationPerCharacter));
    }
    public void DisplayText(DialogueTurn turn) {
        ResetText();
        _textMeshPro.text = turn._text;
        
        _skipText.enabled = true;
    }
    
    
    public void ResetText() {
        _skipText.enabled = false;
        
        if (_coroutine != null) {
            StopCoroutine(_coroutine);
        }
        _isTyping = false;
        _displayedText = "";
        _textMeshPro.text = _displayedText;
    }

    public IEnumerator TypeText(string text, float durationPerCharacter) {
        _isTyping = true;
        _textMeshPro.text = "";
        for (int i = 0; i < text.Length; i++) {
            _displayedText += text[i];
            _textMeshPro.text = _displayedText;
            yield return new WaitForSeconds(durationPerCharacter);
        }

        _skipText.enabled = true;
        _isTyping = false;
    }
}
