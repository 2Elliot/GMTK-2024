using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogueUI _dialogueUI;
    [SerializeField] private TextTypingAnimator _textTypingAnimator;
    [Header("Dialogue Queue")]
    [SerializeField] private List<Dialogue> _dialoguesQueue = new List<Dialogue>();
    
    private Action _functionToCall;
    
    private void AddDialogueToQueue(Dialogue dialogue) {
        if (_dialoguesQueue.Contains(dialogue)) return;
        _dialoguesQueue.Add(dialogue);
    }
    public Dialogue GetDialogueByIndex(int index) {
        if (index < 0 || index >= _dialoguesQueue.Count) return null;
        return _dialoguesQueue[index];
    }
    
    private bool _isPlayingDialogue = false;
    private IEnumerator PlayDialogueCoroutine(Dialogue dialogue) {
        _isPlayingDialogue = true;
        for (int i = 0; i < dialogue._textTurns.Count; i++) {
            _textTypingAnimator.ResetText();
            _dialogueUI.SetDialogue(dialogue, i);
            _dialogueUI.ShowDialogueBox();
            yield return new WaitForSeconds(_dialogueUI.GetDialogueBoxShowTime());
            yield return new WaitForSeconds(dialogue._textTurns[i]._startDelay);
            _textTypingAnimator.StartTyping();
            yield return new WaitForSeconds(dialogue._textTurns[i]._duration);
            yield return new WaitForSeconds(dialogue._textTurns[i]._endDelay);
            _dialogueUI.HideDialogueBox();
            // print("called hide dialogue box");
            yield return new WaitForSeconds(_dialogueUI.GetDialogueBoxHideTime());
        }
        _dialoguesQueue.RemoveAt(0);
        _isPlayingDialogue = false;
        _functionToCall();
    }
    public void PlayDialogue(Dialogue dialogue, bool forcePlay = false, Action callbackFunction = null) {
        _functionToCall = null;
        if (callbackFunction != null) _functionToCall = callbackFunction;
        
        if (forcePlay) {
            _dialoguesQueue.Clear();
            StopAllCoroutines();
            _isPlayingDialogue = false;
            AddDialogueToQueue(dialogue);
        }
        else {
            AddDialogueToQueue(dialogue);
        }
    }

    private void Update() {
        if (_dialoguesQueue.Count > 0 && !_isPlayingDialogue) {
            StartCoroutine(PlayDialogueCoroutine(_dialoguesQueue[0]));
        }
    }

    private void SetCallbackFunction(Action function) {
        _functionToCall = function;
    }
    
}
