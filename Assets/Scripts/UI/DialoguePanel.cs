using Common;

namespace UI
{
	internal class DialoguePanel : UiPanelBase
	{

		protected override void Start()
		{
			base.Start();
			GameState.OnChanged += OpenOnDialogue;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			GameState.OnChanged -= OpenOnDialogue;
		}

		private void OpenOnDialogue(GameStateType obj)
		{
			if (obj == GameStateType.InDialogue) Open();
			else Close();
		}
	}
}