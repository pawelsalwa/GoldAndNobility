using System;
using InventorySystem;

namespace TradeSystem
{
    public interface ITradeController
    {
        event Action<TradeEntity> OnTradeStartedWith;
        
        event Action<TradeOffer> OnNpcOfferCreated;
        event Action<TradeOffer> OnPlayerOfferCreated;
        
        event Action<TradeOffer> OnNpcOfferAccepted;
        event Action<TradeOffer> OnPlayerOfferAccepted;

        void BeginTradeBetween(TradeEntity player, TradeEntity npc);
        void GenerateNpcSellOffer(ItemStack item);
        void GeneratePlayerSellOffer(ItemStack item);
        void AcceptCurrentOffer();
    }
}