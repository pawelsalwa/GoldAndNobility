using Common;
using Common.GameInput;
using UnityEngine.UI;

namespace UI.MainMenu
{
    internal class MainMenu : UiPanelBase
    {

        public Button startBtn;
        public Button loadBtn;
        public Button quitBtn;
        
        protected override bool ShowOnAwake => true;

        protected override void Start()
        {
            base.Start();
            startBtn.onClick.AddListener(OnStartBtn);
            CharacterInput.enabled = false;
        }

        private void OnStartBtn() => ServiceLocator.RequestService<ISceneLoader>().LoadGameScene();

    }
}
