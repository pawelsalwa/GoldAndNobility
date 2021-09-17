using System;

namespace Dialogue
{
	public enum Talker { Player, Npc}

	[Serializable]
	public class Quote
	{
		public string text;
		public Talker talker;
	}

}