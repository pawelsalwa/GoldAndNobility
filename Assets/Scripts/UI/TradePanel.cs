using Common;
using DialogueSystem;
using GameManagement;
using UnityEngine.UI;

namespace UI
{
    internal class TradePanel : UiPanelBase
    {
        // private ITradeManager service;
        public Button finishTradeButton;

        protected override void Start()
        {
            base.Start();
            GameState.OnChanged += OnStateChanged;
            finishTradeButton.onClick.AddListener(FinishTrade);
        }

        private void FinishTrade()
        {
            Close();
            GameState.CancelState(GameStateType.Trading); // this should go to some trade manager class and it will help to clean this up :)
            ServiceLocator.RequestService<IDialogueController>().Skip(); // hack i guess? :( we need to find a way to properly get back to dialogue from trading
        }

        protected override void OnDestroy()
        {
            GameState.OnChanged -= OnStateChanged;
            finishTradeButton.onClick.RemoveListener(FinishTrade);
        }

        private void OnStateChanged(GameStateType obj)
        {
            if (obj == GameStateType.Trading) Open();
        }
    }
}
