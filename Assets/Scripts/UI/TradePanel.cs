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
        private ITradeManager service;

        protected override void Start()
        {
            base.Start();
            service = ServiceLocator.RequestService<ITradeManager>();
            finishTradeButton.onClick.AddListener(service.FinishTrade);
            service.OnTradeStarted += TradeStarted;
            service.OnTradeFinished += Close; 
        }

        protected override void OnDestroy()
        {
            finishTradeButton.onClick.RemoveListener(service.FinishTrade);
            service.OnTradeStarted -= TradeStarted;
            service.OnTradeFinished -= Close;
        }

        protected override void OnOpened() => inventoryPanel.Open();
        protected override void OnClosed() => inventoryPanel.Close();

        private void TradeStarted(TradeEntity obj)
        {
            npcInventoryUi.Init(obj.inventory);
            Open();
        }

        // private void FinishTrade()
        // {
        //     Close();
        //     // GameState.CancelState(GameStateType.Trading); // this should go to some trade manager class and it will help to clean this up :)
        //     ServiceLocator.RequestService<IDialogueManager>().Skip(); // hack i guess? :( we need to find a way to properly get back to dialogue from trading
        // }
    }
}
