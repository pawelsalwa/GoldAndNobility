using System;
using System.Collections.Generic;
using Common;
using Common.Attributes;
using Common.GameInput;
using UnityEngine;

namespace Dialogue
{
    /// <summary> through events we can handle ui/logic/etc </summary>
    [PersistentComponent]
    public class DialogueController : MonoBehaviour, IDialogueController
    {
        
        public event Action OnDialogueStarted;
        public event Action<Quote> OnQuote;
        public event Action<List<Quote>> OnPlayerChoicesAppear;
        public event Action OnDialogueEnded;
        
        private bool active = false;
        private DialogueData currentDialogue;
        private Quote currentLine;

        private bool currentLineIsLast => currentLineIdx == currentDialogue.quotes.Count - 1;
        private int currentLineIdx;
        private GameStateType stateCache;
        private IInputSwitchService inputService;
        private InputFocus inputCache;

        private DialogueController() => ServiceLocator.RegisterService<IDialogueController>(this);

        private void Awake() => inputService = ServiceLocator.RequestService<IInputSwitchService>();

        public void StartDialogue(DialogueData data)
        {
            if (!data) return;
            inputCache = inputService.current;
            inputService.SetInputFocus(InputFocus.Dialogue);
            stateCache = GameState.Current;
            GameState.Current = GameStateType.InDialogue;
            currentDialogue = data;
            active = true;
            currentLineIdx = 0;
            OnDialogueStarted?.Invoke();
            GoToNextLine();
        }

        private void Update()
        {
            if (!active) return;
            if (DialogueInput.advanceDialogue) GoToNextLine();
        }

        private void GoToNextLine()
        {
            if (currentDialogue == null) return;
            if (currentLine == null) currentLine = currentDialogue.quotes[currentLineIdx = 0];
            else if (currentLineIsLast)
            {
                EndDialogue();
                return;
            }
            else currentLine = currentDialogue.quotes[++currentLineIdx];
            OnQuote?.Invoke(currentLine);
        }

        public void ChooseQuote(Quote quote)
        {
            
        }
        
        private void EndDialogue()
        {
            inputService.SetInputFocus(inputCache);
            GameState.Current = stateCache;
            currentDialogue = null;
            currentLine = null;
            currentLineIdx = 0;
            active = false;
            OnDialogueEnded?.Invoke();
        }
    }
}
