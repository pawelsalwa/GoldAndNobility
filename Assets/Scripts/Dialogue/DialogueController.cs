using System;
using System.Collections.Generic;
using Common;
using Common.Attributes;
using GameInput;
using UnityEngine;

namespace Dialogue
{
    /// <summary> through events we can handle ui/audio/etc </summary>
    [PersistentComponent]
    public class DialogueController : MonoBehaviour, IDialogueController
    {
        
        public event Action<DialogueData> OnDialogueStarted;
        public event Action<Quote> OnQuoteStarted;
        public event Action<List<Quote>> OnPlayerQuotesAppear;
        public event Action OnDialogueEnded;
        
        private bool active = false;

        private GameStateType stateCache;
        private IInputSwitchService inputService;
        private InputFocus inputCache;

        private readonly List<Quote> currentChoices = new List<Quote>();
        private bool isShowingChoices => currentChoices.Count > 0;
        
        public DialogueData CurrentDialogue { get; private set; }
        private Quote currentQuote;

        private DialogueController() => ServiceLocator.RegisterService<IDialogueController>(this);

        private void Start() => inputService = ServiceLocator.RequestService<IInputSwitchService>();

        public void StartDialogue(DialogueData data)
        {
            if (!data) return;
            inputCache = inputService.current;
            inputService.SetInputFocus(InputFocus.Dialogue);
            stateCache = GameState.Current;
            GameState.Current = GameStateType.InDialogue;
            CurrentDialogue = data;
            active = true;
            OnDialogueStarted?.Invoke(data);
            SayQuote(data.entryQuote);
        }

        public void ChoosePlayerQuote(Quote quote)
        {
            if (!currentChoices.Contains(quote)) throw new ArgumentException("Cant choose player quote that is not within current choices.");
            currentChoices.Clear(); // important to clear choices when we're done choosing
            SayQuote(quote);
        }

        public void Skip()
        {
            if (!CurrentDialogue) throw new Exception("Cant skip, current dialogue is null");
            if (isShowingChoices) throw new Exception("Cant call skip dialogue when multiple choices are shown.");
            GoToNextQuote();
        }

        private void SayQuote(Quote quote)
        {
            currentQuote = quote;
            OnQuoteStarted?.Invoke(quote);
        }

        private void Update()
        {
            if (!active) return;
            if (DialogueInput.advanceDialogue && !isShowingChoices) Skip();
        }

        private void GoToNextQuote()
        {
            var nextQuotes = CurrentDialogue.GetNextQuotesFor(currentQuote);
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
            inputService.SetInputFocus(inputCache);
            GameState.Current = stateCache;
            CurrentDialogue = null;
            active = false;
            OnDialogueEnded?.Invoke();
        }
    }
}
