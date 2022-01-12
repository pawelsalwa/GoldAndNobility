using DialogueSystem;
using UnityEngine;

namespace QuestSystem
{

	public struct TalkStageSetup
	{
		public DialogueData dialogue;
	}
	
	[CreateAssetMenu(menuName = "ScriptableObject/QuestStage/TalkStage", fileName = "TalkStage", order = 0)]
	public class TalkStage : QuestStageBase
	{

		private DialogueData dialogue;
		private Quote quote;

		public void Init(TalkStageSetup setup)
		{
			dialogue = setup.dialogue;
		}

		protected override void Enabled()
		{
			quote = new Quote {talker = Talker.Npc, text = "Do this quest for me please..."};
			dialogue.AddQuote(quote);
			dialogue.AddEdge(dialogue.entryQuote, quote);
		}

		protected override void Completed()
		{
			// dialogue.RemoveEdge(dialogue.entryQuote, quote);
			// dialogue.RemoveQuote(quote);
		}
	}
}