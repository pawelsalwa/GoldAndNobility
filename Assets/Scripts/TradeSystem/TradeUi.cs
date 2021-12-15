using Common;
using InventorySystem;
using UnityEngine;

namespace TradeSystem
{
    internal class TradeUi : MonoBehaviour
    {
        
        public InventoryUi npcInventoryUi;
        public InventoryUi playerInventoryUi;
        
        private ITradeController service;
        
        private void Start()
        {
            service = ServiceLocator.RequestService<ITradeController>();
            service.OnTradeStartedWith += TradeStartedWith;
            playerInventoryUi.OnItemSelected += GeneratePlayerOffer;
            npcInventoryUi.OnItemSelected += GenerateNpcOffer;
        }

        private void OnDestroy()
        {
            service.OnTradeStartedWith -= TradeStartedWith;
            playerInventoryUi.OnItemSelected -= GeneratePlayerOffer;
            npcInventoryUi.OnItemSelected -= GenerateNpcOffer;
        }

        private void TradeStartedWith(TradeEntity obj) => npcInventoryUi.Init(obj.inventory);
        private void GeneratePlayerOffer(ItemStack obj) => service.GeneratePlayerSellOffer(obj);
        private void GenerateNpcOffer(ItemStack obj) => service.GenerateNpcSellOffer(obj);
    }
}