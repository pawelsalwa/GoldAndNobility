using System;
using InventorySystem;

namespace GameManagement
{
    public interface ITradeManager
    {
        event Action<TradeEntity> OnTradeStarted;
        event Action OnTradeFinished;
        event Action<TradeOffer> OnNpcOfferCreated;
        event Action<TradeOffer> OnPlayerOfferCreated;
        event Action<TradeOffer> OnNpcOfferAccepted;
        event Action<TradeOffer> OnPlayerOfferAccepted;
        
        void BeginTrade(TradeEntity target);
        void FinishTrade();
        void GenerateNpcSellOffer(ItemStack item);
        void GeneratePlayerSellOffer(ItemStack item);
        void AcceptCurrentOffer();
    }
}