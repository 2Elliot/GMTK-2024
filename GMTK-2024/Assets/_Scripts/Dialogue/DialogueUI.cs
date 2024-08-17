using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private Animator _dialogueBoxAnimator;
    [SerializeField] private AnimationClip _dialogueBoxShowClip;
    [SerializeField] private AnimationClip _dialogueBoxHideClip;
    [SerializeField] private TMPro.TextMeshProUGUI _characterNameText;
    [SerializeField] private TextTypingAnimator _textTypingAnimator;

    public void SetDialogue(Dialogue dialogue, int turnIndex) {
        DialogueTurn dialogueTurn = dialogue._textTurns[turnIndex];
        _characterNameText.text = $"<color=#{ColorUtility.ToHtmlStringRGB(dialogueTurn._character.NameColor)}>{dialogueTurn._character.CharacterName}:</color>";
        _textTypingAnimator._text = $"{dialogueTurn._text}";
        _textTypingAnimator._typingSpeed = (dialogueTurn._duration) / dialogueTurn._text.Length;
    }
    
    public void ShowDialogueBox() {
        _dialogueBoxAnimator.SetBool("Show", true);
    }
    
    public void HideDialogueBox() {
        _dialogueBoxAnimator.SetBool("Show", false);
    }
    
    public float GetDialogueBoxShowTime() {
        return _dialogueBoxShowClip.length;
    } 
    
    public float GetDialogueBoxHideTime() {
        return _dialogueBoxHideClip.length;
    }

}
