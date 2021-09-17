using System;
using Common;
using Common.Attributes;
using UnityEngine;

namespace Dialogue
{
    /// <summary> through events we can handle ui/logic/etc </summary>
    [PersistentComponent]
    public class DialogueController : MonoBehaviour, IDialogueController
    {
        
        public event Action OnDialogueStarted;
        public event Action<Quote> OnQuote;
        public event Action OnDialogueEnded;
        private bool active = false;
        private DialogueData currentDialogue;
        private Quote currentLine;

        private bool currentLineIsLast => currentLineIdx == currentDialogue.quotes.Count - 1;
        private int currentLineIdx;
        
        private DialogueController() => ServiceLocator.RegisterService<IDialogueController>(this);

        public void StartDialogue(DialogueData data)
        {
            if (!data) return;
            currentDialogue = data;
            active = true;
            currentLineIdx = 0;
            OnDialogueStarted?.Invoke();
            GoToNextLine();
        }

        private void Update()
        {
            if (!active) return;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) GoToNextLine();
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


        private void EndDialogue()
        {
            currentDialogue = null;
            currentLine = null;
            currentLineIdx = 0;
            active = false;
            OnDialogueEnded?.Invoke();
        }
    }
}
