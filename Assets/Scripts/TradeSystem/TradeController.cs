using System;
using System.Linq;
using Common.Attributes;
using InventorySystem;
using UnityEngine;

namespace TradeSystem
{
    [GameService(typeof(ITradeController))]
    internal class TradeController : MonoBehaviour, ITradeController
    {
        public event Action<TradeEntity> OnTradeStartedWith;
        public event Action<TradeOffer> OnNpcOfferCreated;
        public event Action<TradeOffer> OnPlayerOfferCreated;
        public event Action<TradeOffer> OnNpcOfferAccepted;
        public event Action<TradeOffer> OnPlayerOfferAccepted;

        private TradeEntity npcEntity;
        private TradeEntity playerEntity;
        private readonly TradeOffer tradeOffer = new(); // since only one trade offer need to be shown we can cache and reuse it

        public void BeginTradeBetween(TradeEntity player, TradeEntity npc)
        {
            npcEntity = npc;
            playerEntity = player;
            OnTradeStartedWith?.Invoke(npc);
        }

        public void GenerateNpcSellOffer(ItemStack item)
        {
            if (npcEntity.inventory.Items.All(x => x != item)) throw new ArgumentException("Cant sell item thats not within seller inventory!");
            tradeOffer.isPlayerOffer = false;
            tradeOffer.itemToSell = item; 
            tradeOffer.pricePerUnit = npcEntity.HowMuchGoldIWantFor(item); 
            tradeOffer.maxItemsSellCount = item.count;
            OnNpcOfferCreated?.Invoke(tradeOffer);
        }

        public void GeneratePlayerSellOffer(ItemStack item)
        {
            if (playerEntity.inventory.Items.All(x => x != item)) throw new ArgumentException("Cant sell item thats not within seller inventory!");
            tradeOffer.isPlayerOffer = true;
            tradeOffer.itemToSell = item;
            tradeOffer.pricePerUnit = playerEntity.HowMuchGoldIWantFor(item);
            tradeOffer.maxItemsSellCount = item.count;
            OnPlayerOfferCreated?.Invoke(tradeOffer);
        }

        public void AcceptCurrentOffer()
        {
            if (tradeOffer.isPlayerOffer)
            {
                var succ = npcEntity.inventory.TryAddItem(tradeOffer.itemToSell.data, tradeOffer.itemsSellCount);
                if (!succ) throw new NotImplementedException("not implemented case where inventory is full");
                playerEntity.inventory.RemoveItems(tradeOffer.itemToSell, tradeOffer.itemsSellCount);
                playerEntity.gold.amount += tradeOffer.totalGoldValue;
                npcEntity.gold.amount -= tradeOffer.totalGoldValue;
                OnPlayerOfferAccepted?.Invoke(tradeOffer);
            }
            else
            {
                var succ = playerEntity.inventory.TryAddItem(tradeOffer.itemToSell.data, tradeOffer.itemsSellCount);
                if (!succ) throw new NotImplementedException("not implemented case where inventory is full");
                npcEntity.inventory.RemoveItems(tradeOffer.itemToSell, tradeOffer.itemsSellCount);
                npcEntity.gold.amount += tradeOffer.totalGoldValue;
                playerEntity.gold.amount -= tradeOffer.totalGoldValue;
                OnNpcOfferAccepted?.Invoke(tradeOffer);
            }
        }
    }
}