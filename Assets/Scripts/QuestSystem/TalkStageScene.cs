using DialogueSystem;

namespace QuestSystem
{
	public class TalkStageScene : QuestStageSceneBase
	{

		public DialogueEntity speaker;
		public DialogueData questDialogueTemplate;
		private DialogueData questRuntimeDialogue;

		protected override void Enabled()
		{
			questRuntimeDialogue = Instantiate(questDialogueTemplate);
			speaker.AddRuntimeDialogueBranch(questRuntimeDialogue);
		}

		protected override void Completed()
		{
			speaker.RemoveRuntimeDialogueBranch(questRuntimeDialogue);
		}
	}
}