using Common;
using Common.Fsm;
using Common.GameInput;
using UnityEngine.UI;

namespace UI
{
	internal class PauseGamePanel : UiPanelBase
	{

		public Button returnToGameBtn;
		public Button mainMenuBtn;
		public Button quitBtn;
		
		private IGameStates gameState;
		private IGameFsm gameFsm;

		protected override void Start()
		{
			base.Start();
			gameState = ServiceLocator.RequestService<IGameStates>();
			gameFsm = ServiceLocator.RequestService<IGameFsm>();
			gameState.GamePaused.OnEntered += Open;
			gameState.GamePaused.OnExited += Close;

			returnToGameBtn.onClick.AddListener(ReturnToGame);
			mainMenuBtn.onClick.AddListener(ReturnToMainMenu);
			quitBtn.onClick.AddListener(Quit);
		}


		private void ReturnToMainMenu() => ServiceLocator.RequestService<ISceneLoader>().LoadMainMenu();

		private void ReturnToGame() => gameFsm.UnpauseGame();

		private void Quit() { throw new System.NotImplementedException(); }

		protected override void OnDestroy()
		{
			gameState.GamePaused.OnEntered -= Open;
			gameState.GamePaused.OnExited -= Close;
		}

		protected override void UpdateActive()
		{
			if (UiInput.unpauseGame) gameFsm.UnpauseGame();
		}

	}
}