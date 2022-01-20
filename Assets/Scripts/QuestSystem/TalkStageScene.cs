using DialogueSystem;

namespace QuestSystem
{
	public class TalkStageScene : QuestStage
	{

		public DialogueEntity speaker;
		public DialogueData questDialogueTemplate;

		private DialogueData questRuntimeDialogue;

		protected override void Enabled()
		{
			questRuntimeDialogue = Instantiate(questDialogueTemplate);
			speaker.AddRuntimeDialogueBranch(questRuntimeDialogue);
			foreach (var q in questRuntimeDialogue.EndingQuotes) 
				q.OnSaidCallback += Complete;
		}

		protected override void Disabled()
		{
			speaker.RemoveRuntimeDialogueBranch(questRuntimeDialogue);
			foreach (var q in questRuntimeDialogue.EndingQuotes) 
				q.OnSaidCallback -= Complete;
		}

		protected override void Completed()
		{
			// speaker.RemoveRuntimeDialogueBranch(questRuntimeDialogue);
		}
	}
}