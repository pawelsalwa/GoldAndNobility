using System;
using System.Collections.Generic;
using Common;
using Common.Attributes;
using Common.GameInput;
using UnityEngine;

namespace Dialogue
{
    /// <summary> through events we can handle ui/audio/etc </summary>
    [PersistentComponent]
    public class DialogueController : MonoBehaviour, IDialogueController
    {
        
        public event Action<DialogueData> OnDialogueStarted;
        public event Action<Quote> OnQuoteStarted;
        public event Action<List<Quote>> OnPlayerChoicesAppear;
        public event Action OnDialogueEnded;
        
        private bool active = false;
        
        private bool currentLineIsLast => currentLineIdx == CurrentDialogue.quotes.Count - 1;
        private int currentLineIdx;
        private GameStateType stateCache;
        private IInputSwitchService inputService;
        private InputFocus inputCache;
        
        public DialogueData CurrentDialogue { get; private set; }
        public Quote CurrentQuote { get; private set; }

        private DialogueController() => ServiceLocator.RegisterService<IDialogueController>(this);

        private void Awake() => inputService = ServiceLocator.RequestService<IInputSwitchService>();

        public void StartDialogue(DialogueData data)
        {
            if (!data) return;
            inputCache = inputService.current;
            inputService.SetInputFocus(InputFocus.Dialogue);
            stateCache = GameState.Current;
            GameState.Current = GameStateType.InDialogue;
            CurrentDialogue = data;
            active = true;
            SayQuote(data.entryQuote);
            currentLineIdx = 0;
            OnDialogueStarted?.Invoke(data);
            // GoToNextLine();
        }

        public void SayQuote(int idx)
        {
            throw new NotImplementedException(); 
            
        }

        public void Skip()
        {
            GoToNextLine();
        }

        private void SayQuote(Quote quote)
        {
            CurrentQuote = quote;
            OnQuoteStarted?.Invoke(quote);
        }

        private void Update()
        {
            if (!active) return;
            if (DialogueInput.advanceDialogue) GoToNextLine();
        }

        private void GoToNextLine()
        {
            if (CurrentDialogue == null) return;
            if (CurrentQuote == null) CurrentQuote = CurrentDialogue.quotes[currentLineIdx = 0];
            else if (currentLineIsLast)
            {
                EndDialogue();
                return;
            }
            else CurrentQuote = CurrentDialogue.quotes[++currentLineIdx];
            OnQuoteStarted?.Invoke(CurrentQuote);
        }

        public void ChooseQuote(Quote quote)
        {
            
        }

        private void OnSpeadk()
        {
            
        }
        
        private void EndDialogue()
        {
            inputService.SetInputFocus(inputCache);
            GameState.Current = stateCache;
            CurrentDialogue = null;
            CurrentQuote = null;
            currentLineIdx = 0;
            active = false;
            OnDialogueEnded?.Invoke();
        }
    }
}
