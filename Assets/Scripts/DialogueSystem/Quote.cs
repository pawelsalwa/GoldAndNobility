using System;
using UnityEngine;

namespace DialogueSystem
{
	public enum Talker { Player, Npc}

	[Serializable]
	public class Quote
	{
		public event Action OnSaidCallback;
		public string text = "Hello wanderer...";
		public Talker talker;
		public Rect pos;
		public DialogueAction dialogueAction;

		public bool isDialogueAction => dialogueAction;

		internal void OnSaid() => OnSaidCallback?.Invoke();
	}
}