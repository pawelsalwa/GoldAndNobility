using System;

namespace Dialogue
{
	public interface IDialogueController
	{
		event Action OnDialogueStarted;
		event Action<Quote> OnQuote;
		event Action OnDialogueEnded;
		
		void StartDialogue(DialogueData data);
	}
}