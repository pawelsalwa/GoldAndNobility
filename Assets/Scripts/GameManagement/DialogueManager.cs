using System;
using System.Collections.Generic;
using Common;
using Common.Attributes;
using DialogueSystem;
using GameInput;
using UnityEngine;

namespace GameManagement
{
    /// <summary> I got no idea if thats a good approachm basically this class wraps DialogueSystem functionality providing similar interface that system itself to service locator.
    /// Thanks to that, DialogueSystem doesnt need to have any project related dependancies</summary>
    [GameService(typeof(IDialogueManager))]
    internal class DialogueManager : MonoBehaviour, IDialogueManager
    {
        public event Action<DialogueData> OnDialogueStarted;
        public event Action OnDialogueEnded;
        public event Action<Quote> OnQuoteStarted;
        public event Action<List<Quote>> OnPlayerQuotesAppear;

        /// <summary> Game end of talking to whole DialogueSystem </summary>
        private readonly IDialogueController controller = new DialogueController();

        public void ChoosePlayerQuote(Quote quote) => controller.ChoosePlayerQuote(quote);
        public void Skip() => controller.Skip();

        public void StartDialogue(DialogueData data)
        {
            controller.StartDialogue(data);
            GameState.ChangeState(GameStateType.InDialogue);
        }

        private void Awake()
        {
            controller.OnDialogueStarted += DialogueStarted;
            controller.OnDialogueEnded += DialogueEnded;
            controller.OnQuoteStarted += QuoteStarted;
            controller.OnPlayerQuotesAppear += PlayerQuotesAppear;
        }

        private void DialogueStarted(DialogueData obj) => OnDialogueStarted?.Invoke(obj);

        private void DialogueEnded()
        {
            GameState.CancelState(GameStateType.InDialogue);
            OnDialogueEnded?.Invoke();
        }

        private void OnDestroy()
        {
            controller.OnDialogueStarted -= DialogueStarted;
            controller.OnDialogueEnded -= DialogueEnded;
            controller.OnQuoteStarted -= QuoteStarted;
            controller.OnPlayerQuotesAppear -= PlayerQuotesAppear;
        }

        private void PlayerQuotesAppear(List<Quote> obj) => OnPlayerQuotesAppear?.Invoke(obj);


        private void QuoteStarted(Quote obj) => OnQuoteStarted?.Invoke(obj);

        private void EndDialogueState() => GameState.CancelState(GameStateType.InDialogue);


        private void Update()
        {
            if (DialogueInput.advanceDialogue) controller.Skip();
        }
    }

    public interface IDialogueManager
    {
        event Action<DialogueData> OnDialogueStarted;
        event Action OnDialogueEnded;
        event Action<Quote> OnQuoteStarted;
        event Action<List<Quote>> OnPlayerQuotesAppear;

        void StartDialogue(DialogueData data);
        void ChoosePlayerQuote(Quote quote);
        void Skip();
    }
}