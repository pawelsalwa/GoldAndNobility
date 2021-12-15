using System;
using Common;
using Common.Attributes;
using InventorySystem;
using UnityEngine;

namespace GameManagement
{
    [GameService(typeof(ITradeManager))]
    internal class TradeManager : MonoBehaviour, ITradeManager
    {
        public event Action<TradeEntity> OnTradeStarted;
        public event Action<TradeOffer> OnOfferCreated;
        public event Action<TradeOffer> OnOfferAccepted;
        public event Action OnTradeFinished;

        private TradeEntity currentTarget;
        private readonly TradeOffer tradeOffer = new(); // since only one trade offer need to be shown we can cache and reuse it

        public void BeginTrade(TradeEntity target)
        {
            currentTarget = target;
            GameState.ChangeState(GameStateType.Trading);
            OnTradeStarted?.Invoke(target);
        }

        public void FinishTrade()
        {
            GameState.CancelState(GameStateType.Trading);
            OnTradeFinished?.Invoke();
        }

        public void GenerateOfferFor(ItemStack item)
        {
            tradeOffer.itemToSell = item;
            tradeOffer.pricePerUnit = (int) (item.data.defaultPrice * currentTarget.priceValueMultiplier);
            tradeOffer.maxItemsSellCount = item.count;
            OnOfferCreated?.Invoke(tradeOffer);
        }

        public void AcceptCurrentOffer()
        {
            if (GameState.Current != GameStateType.Trading) throw new Exception("wtf");
            // if (tradeOffer.itemsSellCount == 0) return;
            var service = ServiceLocator.RequestService<IPlayerCharacterSingleton>();
            service.tradeEntity.inventory.RemoveItems(tradeOffer.itemToSell, tradeOffer.itemsSellCount);
            service.tradeEntity.gold.amount += (int) (tradeOffer.itemsSellCount * currentTarget.priceValueMultiplier);
            OnOfferAccepted?.Invoke(tradeOffer);
        }
    }

    public interface ITradeManager
    {
        event Action<TradeEntity> OnTradeStarted;
        event Action<TradeOffer> OnOfferCreated;
        event Action<TradeOffer> OnOfferAccepted;
        event Action OnTradeFinished;
        void BeginTrade(TradeEntity target);
        void FinishTrade();
        void GenerateOfferFor(ItemStack item);
        void AcceptCurrentOffer();
    }
}