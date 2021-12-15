using System;
using Common;
using DialogueSystem;
using TradeSystem;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(menuName = "ScriptableObject/DialogueAction/TradingDialogueAction", fileName = "TradingDialogueAction", order = 0)]
    public class TradingDialogueAction : DialogueAction
    {
        private ITradeManager service;
        public TradeEntity tradeEntity { set; private get; }

        public override void BeginDialogueAction()
        {
            if (tradeEntity == null) throw new Exception("Trade action requires current trader to be set not to null.");
            service = ServiceLocator.RequestService<ITradeManager>();
            service.OnTradeFinished += OnTradeFinished;
            service.BeginTrade(tradeEntity);

            void OnTradeFinished()
            {
                service.OnTradeFinished -= OnTradeFinished;
                base.FinishDialogueAction();
            }
        }
    }
}
