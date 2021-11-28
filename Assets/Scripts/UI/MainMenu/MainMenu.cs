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

        protected override void OnOpened() => GameState.Current = GameStateType.MainMenu;

        protected override void Start()
        {
            base.Start();
            startBtn.onClick.AddListener(OnStartBtn);
            ServiceLocator.RequestService<IInputSwitchService>().SetInputFocus(InputFocus.UI);
        }

        private void OnStartBtn() => ServiceLocator.RequestService<ISceneLoader>().LoadGameScene();

    }
}
