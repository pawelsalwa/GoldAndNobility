using System;
using Common;
using Common.Attributes;
using TradeSystem;
using UnityEngine;

namespace GameManagement
{
    [GameService(typeof(ITradeManager))]
    internal class TradeManager : MonoBehaviour, ITradeManager
    {
        public event Action OnTradeStarted;
        public event Action OnTradeFinished;

        private TradeEntity player;
        private ITradeController service;

        private void Start()
        {
            player = ServiceLocator.RequestService<IPlayerCharacterSingleton>().tradeEntity;
            service = ServiceLocator.RequestService<ITradeController>();
        }

        public void BeginTrade(TradeEntity npc)
        {
            service.BeginTradeBetween(player, npc);
            GameState.ChangeState(GameStateType.Trading);
            OnTradeStarted?.Invoke();
        }

        public void FinishTrade()
        {
            GameState.CancelState(GameStateType.Trading);
            OnTradeFinished?.Invoke();
        }
    }
}