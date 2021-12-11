using System;
using Common;
using Common.Attributes;
using RuntimeData;
using UnityEngine;

namespace TradeSystem
{
    [GameService(typeof(ITradeManager))]
    internal class TradeManager : MonoBehaviour, ITradeManager
    {
        public event Action OnPlayerInventoryOpened;
        public event Action<TradeEntity> OnNpcInventoryOpened;

        public void BeginTrade(TradeEntity target)
        {
            // open player inventory
            
            // open npc inventory
            OnPlayerInventoryOpened?.Invoke();
            OnNpcInventoryOpened?.Invoke(target);
            
            ServiceLocator.RequestService<IInventory>();
            
            GameState.ChangeState(GameStateType.Trading);
        }
    }

    public interface ITradeManager
    {
        event Action OnPlayerInventoryOpened;
        event Action<TradeEntity> OnNpcInventoryOpened;
        
        void BeginTrade(TradeEntity target);
    }
}