using System;
using System.Collections;
using System.Collections.Generic;
using InputHandler;
using TMPro;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogueUI _dialogueUI;
    [SerializeField] private TextTypingAnimator _textTypingAnimator;
    private PlayerInputActions _inputActions; 
    [Header("Dialogue Queue")]
    [SerializeField] private List<Dialogue> _dialoguesQueue = new List<Dialogue>();
    [SerializeField] private int _currentDialogueTurnIndex = 0;
    
    private Action _functionToCall;

    private void OnEnable() {
        _inputActions = InputReader.Instance.InputActions;
        _inputActions.Player.Space.performed += _ => AdvanceDialogue();
    }
    private void OnDisable() {
        _inputActions.Player.Space.performed -= _ => AdvanceDialogue();
    }

    private void AddDialogueToQueue(Dialogue dialogue) {
        if (_dialoguesQueue.Contains(dialogue)) return;
        _dialoguesQueue.Add(dialogue);
    }
    public Dialogue GetDialogueByIndex(int index) {
        if (index < 0 || index >= _dialoguesQueue.Count) return null;
        return _dialoguesQueue[index];
    }
    
    private bool _isPlayingDialogue = false;
    private bool _firstLoop = true;
    private IEnumerator PlayDialogueCoroutine(Dialogue dialogue) 
    {
        _isPlayingDialogue = true;
        _firstLoop = true;
        _currentDialogueTurnIndex = 0;
        _dialogueUI.SetDialogue(dialogue, _currentDialogueTurnIndex);
        _dialogueUI.ShowDialogueBox();
        yield return new WaitForSeconds(_dialogueUI.GetDialogueBoxShowTime());
        DialogueTurn currentTurn;
        while (_currentDialogueTurnIndex < dialogue._textTurns.Count) {
            currentTurn = dialogue._textTurns[_currentDialogueTurnIndex];
            if (_firstLoop) {
                _textTypingAnimator.ResetText();
                _dialogueUI.SetDialogue(dialogue, _currentDialogueTurnIndex);
                _textTypingAnimator.StartTyping(currentTurn, currentTurn._duration / currentTurn._text.Length);
                _firstLoop = false;
            }
            yield return null;
        }
        _dialogueUI.HideDialogueBox();
        yield return new WaitForSeconds(_dialogueUI.GetDialogueBoxHideTime());
        _dialoguesQueue.RemoveAt(0);
        _isPlayingDialogue = false;
        _functionToCall();
    }

    private void AdvanceDialogue() {
        if (!_isPlayingDialogue) {
            print("AdvanceDialogue() Not advancing dialogue, nothing playing");
            return;
        }
        if (_textTypingAnimator._isTyping) {
            _textTypingAnimator.DisplayText(_dialoguesQueue[0]._textTurns[_currentDialogueTurnIndex]);
        }
        else {
            _currentDialogueTurnIndex++;
            _firstLoop = true;
        }
        print("AdvanceDialogue() advancing dialogue");
    }
    
    public void PlayDialogue(Dialogue dialogue, bool forcePlay = false, Action dialogueEndCallback = null) {
        _functionToCall = null;
        if (dialogueEndCallback != null) _functionToCall = dialogueEndCallback;
        
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
