	using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueTurn
{
	public Customer _customer;
	[TextArea(3, 20)] public string _text;
	public float _startDelay {
		get { return 0.5f; }
	}
	private float timePerCharacter = 0.04f;

	[HideInInspector]
	public float _duration {
		get {
			return _text.Length * timePerCharacter;
		}
	}

}

[Serializable]
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Object")]
public class Dialogue : ScriptableObject
{
	[Tooltip("List of text turns in the dialogue. Each turn will be displayed in the subtitle box.")]
	public List<DialogueTurn> _textTurns;
}