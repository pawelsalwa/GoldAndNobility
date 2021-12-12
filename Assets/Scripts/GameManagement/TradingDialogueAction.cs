using System;
using Common;
using DialogueSystem;
using GameManagement.Interactions;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(menuName = "ScriptableObject/DialogueAction/TradingDialogueAction", fileName = "TradingDialogueAction", order = 0)]
    public class TradingDialogueAction : DialogueAction
    {
        private ITradeManager service;

        private void OnEnable()
        {
        }

        public override void BeginDialogueAction()
        {
            if (!CharacterInteraction.currentTrader) throw new Exception("Trade action requires current trader to be set not to null.");
            service = ServiceLocator.RequestService<ITradeManager>();
            service.OnTradeFinished += OnTradeFinished;
            service.BeginTrade(CharacterInteraction.currentTrader);

            void OnTradeFinished()
            {
                service.OnTradeFinished -= OnTradeFinished;
                base.FinishDialogueAction();
            }
        }
    }
}
