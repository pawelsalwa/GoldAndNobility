using System;
using UnityEngine;

namespace DialogueSystem
{
	public enum Talker { Player, Npc}

	[Serializable]
	public class Quote
	{
		public string title = "quote";
		public string text = "Hello wanderer...";
		public Talker talker;
		public Rect pos;
		public bool isDialogueAction = false;
	}

}