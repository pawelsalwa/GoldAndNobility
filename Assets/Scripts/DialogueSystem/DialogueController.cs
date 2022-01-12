using System;
using System.Collections.Generic;

namespace DialogueSystem
{
    /// <summary> through events we can handle ui/audio/etc </summary>
    public class DialogueController : IDialogueController
    {
        
        public event Action<DialogueData> OnDialogueStarted;
        public event Action<Quote> OnQuoteStarted;
        public event Action<List<Quote>> OnPlayerQuotesAppear;
        public event Action OnDialogueEnded;

        public static readonly IDialogueController instance = new DialogueController();
        
        private bool active = false;

        private readonly List<Quote> currentChoices = new();
        private bool isShowingChoices => currentChoices.Count > 0;
        
        private DialogueData currentDialogue;
        private Quote currentQuote;

        private DialogueController() { }

        public void StartDialogue(DialogueData data)
        {
            if (currentDialogue) throw new ArgumentException("Cant start dialogue in the middle of another.");
            if (!data) throw new ArgumentException("Cant start null dialogue.");
            currentDialogue = data;
            active = true;
            OnDialogueStarted?.Invoke(data);
            SayQuote(data.entryQuote);
        }

        public void ChoosePlayerQuote(Quote quote)
        {
            if (quote == null) throw new ArgumentException("Cant choose null quote.");
            if (!currentChoices.Contains(quote)) throw new ArgumentException("Cant choose player quote that is not within current choices.");
            currentChoices.Clear(); // important to clear choices when we're done choosing
            SayQuote(quote);
        }

        public void Skip()
        {
            if (!currentDialogue) throw new Exception("Cant skip, current dialogue is null");
            if (isShowingChoices) return; // throw new Exception("Cant call skip dialogue when multiple choices are shown.");
            // Debug.Log($"<color=white>skippinggg</color>");
            GoToNextQuote();
        }

        private void SayQuote(Quote quote)
        {
            currentQuote = quote;
            if (quote.isDialogueAction)
            {
                quote.dialogueAction.OnFinished += OnFinished;
                quote.dialogueAction.BeginDialogueAction();

                void OnFinished()
                {
                    quote.dialogueAction.OnFinished -= OnFinished;
                    Skip();
                }
            }
            
            OnQuoteStarted?.Invoke(quote);
        }

        private void GoToNextQuote()
        {
            var nextQuotes = currentDialogue.GetNextQuotesFor(currentQuote);
            if (nextQuotes.Count == 0) EndDialogue();
            if (nextQuotes.Count == 1) SayQuote(nextQuotes[0]);
            if (nextQuotes.Count > 1) ShowPlayerChoices(nextQuotes);
        }

        private void ShowPlayerChoices(List<Quote> nextQuotes)
        {
            currentChoices.Clear();
            currentChoices.AddRange(nextQuotes);
            OnPlayerQuotesAppear?.Invoke(nextQuotes);
        }

        private void EndDialogue()
        {
            currentDialogue = null;
            active = false;
            OnDialogueEnded?.Invoke();
        }

        /// <summary> this just exists so we can have static singleton through interface and keep constructor private </summary>
        public static DialogueController GetTestInstance() => new();
    }
}
