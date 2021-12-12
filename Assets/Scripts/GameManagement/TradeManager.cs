using System;
using Common;
using Common.Attributes;
using UnityEngine;

namespace GameManagement
{
    [GameService(typeof(ITradeManager))]
    internal class TradeManager : MonoBehaviour, ITradeManager
    {
        public event Action<TradeEntity> OnTradeStarted;
        public event Action OnTradeFinished;

        public void BeginTrade(TradeEntity target)
        {
            GameState.ChangeState(GameStateType.Trading);
            OnTradeStarted?.Invoke(target);
        }

        public void FinishTrade()
        {
            GameState.CancelState(GameStateType.Trading);
            OnTradeFinished?.Invoke();
        }
    } 

    public interface ITradeManager
    {
        event Action<TradeEntity> OnTradeStarted;
        event Action OnTradeFinished;
        void BeginTrade(TradeEntity target);
        void FinishTrade();
    }
}