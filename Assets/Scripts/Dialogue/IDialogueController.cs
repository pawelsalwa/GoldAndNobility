using System;
using System.Collections.Generic;

namespace Dialogue
{
	public interface IDialogueController
	{
		event Action OnDialogueStarted;
		event Action<Quote> OnQuote;
		event Action<List<Quote>> OnPlayerChoicesAppear;
		event Action OnDialogueEnded;
		
		void StartDialogue(DialogueData data);
	}
}