using Common;
using GameInput;
using UnityEngine.UI;

namespace UI.MainMenu
{
    internal class MainMenu : UiPanelBase
    {

        public Button startBtn;
        public Button loadBtn;
        public Button quitBtn;
        
        protected override bool ShowOnAwake => true;

        protected override void OnOpened() => GameState.ChangeState(GameStateType.MainMenu);

        protected override void OnClosed() => GameState.CancelState(GameStateType.MainMenu);

        protected override void Start()
        {
            base.Start();
            startBtn.onClick.AddListener(OnStartBtn);
        }

        private void OnStartBtn() => ServiceLocator.RequestService<ISceneLoader>().LoadGameScene();

    }
}
