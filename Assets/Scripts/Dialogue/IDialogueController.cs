using System;
using System.Collections.Generic;

namespace Dialogue
{
	public interface IDialogueController
	{
		event Action<DialogueData> OnDialogueStarted;
		event Action<Quote> OnQuoteStarted;
		event Action<List<Quote>> OnPlayerChoicesAppear;
		event Action OnDialogueEnded;
		
		void StartDialogue(DialogueData data);
		void SayQuote(int idx);
		void Skip();
		
		DialogueData CurrentDialogue { get; }
		Quote CurrentQuote { get; }
	}
}