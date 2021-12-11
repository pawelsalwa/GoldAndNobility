using System;
using System.Collections.Generic;
using Common;
using Common.Attributes;
using DialogueSystem;
using GameInput;
using TradeSystem;
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

        private TradeEntity entity;


        public void ChoosePlayerQuote(Quote quote) => controller.ChoosePlayerQuote(quote);

        public void StartDialogue(DialogueData data, TradeEntity tradeEntity)
        {
            entity = tradeEntity;
            controller.StartDialogue(data);
            GameState.ChangeState(GameStateType.InDialogue);
            controller.StartDialogue(data);
        }

        private void Awake()
        {
            controller.OnDialogueStarted += OnDialogueStarted.Invoke;
            controller.OnDialogueEnded += OnDialogueEnded.Invoke;
            controller.OnQuoteStarted += QuoteStarted;
            controller.OnPlayerQuotesAppear += PlayerQuotesAppear;

        }

        private void OnDestroy()
        {
            controller.OnDialogueStarted -= OnDialogueStarted;
            controller.OnDialogueEnded -= OnDialogueEnded;
            controller.OnQuoteStarted -= QuoteStarted;
        }

        private void PlayerQuotesAppear(List<Quote> obj) => OnPlayerQuotesAppear?.Invoke(obj);


        private void QuoteStarted(Quote obj)
        {
            if (obj.isDialogueAction)
            {
                if (obj.text == "selling_ui_action")
                {
                    ServiceLocator.RequestService<ITradeManager>().BeginTrade(entity);
                }
            }
            OnQuoteStarted?.Invoke(obj);
        }

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
		
        void StartDialogue(DialogueData data, TradeEntity entity);
        void ChoosePlayerQuote(Quote quote);
    }
}