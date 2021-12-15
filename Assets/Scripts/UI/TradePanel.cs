using Common;
using GameManagement;
using InventorySystem;
using UnityEngine.UI;

namespace UI
{
    internal class TradePanel : UiPanelBase
    {
        public Button finishTradeButton;
        public UiPanelBase inventoryPanel;
        public InventoryUi npcInventoryUi;
        public InventoryUi playerInventoryUi;

        private ITradeManager service;

        protected override void Start()
        {
            base.Start();
            service = ServiceLocator.RequestService<ITradeManager>();
            finishTradeButton.onClick.AddListener(service.FinishTrade);
            service.OnTradeStarted += TradeStarted;
            service.OnTradeFinished += Close;
            playerInventoryUi.OnItemSelected += GeneratePlayerOffer;
            npcInventoryUi.OnItemSelected += GenerateNpcOffer;
        }

        protected override void OnDestroy()
        {
            finishTradeButton.onClick.RemoveListener(service.FinishTrade);
            service.OnTradeStarted -= TradeStarted;
            service.OnTradeFinished -= Close;
            playerInventoryUi.OnItemSelected -= GeneratePlayerOffer;
            npcInventoryUi.OnItemSelected -= GenerateNpcOffer;
        }

        protected override void OnOpened() => inventoryPanel.Open();
        protected override void OnClosed() => inventoryPanel.Close();
        
        private void GeneratePlayerOffer(ItemStack obj) => service.GeneratePlayerSellOffer(obj);
        private void GenerateNpcOffer(ItemStack obj) => service.GenerateNpcSellOffer(obj);

        private void TradeStarted(TradeEntity obj)
        {
            npcInventoryUi.Init(obj.inventory);
            Open();
        }
    }
}
