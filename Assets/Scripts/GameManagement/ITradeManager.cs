using System;
using InventorySystem;
using TradeSystem;

namespace GameManagement
{
    public interface ITradeManager
    {
        event Action OnTradeStarted;
        event Action OnTradeFinished;
        
        void BeginTrade(TradeEntity target);
        void FinishTrade();
    }
}