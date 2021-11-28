using System;
using System.Collections.Generic;

namespace Dialogue
{
	public interface IDialogueController
	{
		event Action<DialogueData> OnDialogueStarted;
		event Action OnDialogueEnded;
		event Action<Quote> OnQuoteStarted;
		event Action<List<Quote>> OnPlayerQuotesAppear;
		
		void StartDialogue(DialogueData data);
		void ChoosePlayerQuote(Quote quote);
		void Skip();
		
		DialogueData CurrentDialogue { get; }
	}
}