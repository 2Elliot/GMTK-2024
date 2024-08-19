	using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueTurn
{
	public Customer _customer;
	[TextArea(3, 20)] public string _text;
	[Header("Time before the text starts typing")]
	public float _startDelay;
	[Header("Time it takes to type the text")]
	public float _duration;
}

[Serializable]
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Object")]
public class Dialogue : ScriptableObject
{
	[Tooltip("List of text turns in the dialogue. Each turn will be displayed in the subtitle box.")]
	public List<DialogueTurn> _textTurns;
}