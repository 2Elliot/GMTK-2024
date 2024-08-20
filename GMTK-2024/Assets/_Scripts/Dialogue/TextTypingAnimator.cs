using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class TextTypingAnimator : MonoBehaviour
{
    public TMPro.TextMeshProUGUI _textMeshPro;
    private string _displayedText;
    public bool _isTyping;
    [SerializeField] private bool _useSounds;
    [SerializeField] private List<AudioClip> _typingSounds;

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
            Debug.Log("Enable this to use sounds");
            // if (_useSounds && _typingSounds.Count > 0) {
            //     int randomIndex = UnityEngine.Random.Range(0, _typingSounds.Count);
            //     MMSoundManagerSoundPlayEvent.Trigger(_typingSounds[randomIndex], MMSoundManager.MMSoundManagerTracks.Sfx, transform.position, soloSingleTrack: false);
            // }
            yield return new WaitForSeconds(durationPerCharacter);
        }

        _skipText.enabled = true;
        _isTyping = false;
    }
}
