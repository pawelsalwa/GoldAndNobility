using Common;

namespace UI
{
    internal class HudPanel : UiPanelBase
    {
        protected override bool ShowOnAwake => true;

        protected override void Start()
        {
            base.Start();
            GameState.OnChanged += ChangedState;
            ChangedState(GameState.Current);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameState.OnChanged -= ChangedState;
        }

        private void ChangedState(GameStateType obj)
        {
            if (obj == GameStateType.InGame) Open();
            else Close();
        }
    }
}