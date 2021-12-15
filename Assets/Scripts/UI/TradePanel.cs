using Common;
using GameManagement;
using UnityEngine.UI;

namespace UI
{
    internal class TradePanel : UiPanelBase
    {
        public Button finishTradeButton;
        public UiPanelBase inventoryPanel;

        private ITradeManager service;

        protected override void Start()
        {
            base.Start();
            service = ServiceLocator.RequestService<ITradeManager>();
            finishTradeButton.onClick.AddListener(service.FinishTrade);
            service.OnTradeStarted += Open;
            service.OnTradeFinished += Close;
        }

        protected override void OnDestroy()
        {
            finishTradeButton.onClick.RemoveListener(service.FinishTrade);
            service.OnTradeStarted -= Open;
            service.OnTradeFinished -= Close;
        }

        protected override void OnOpened() => inventoryPanel.Open();
        protected override void OnClosed() => inventoryPanel.Close();
    }
}
