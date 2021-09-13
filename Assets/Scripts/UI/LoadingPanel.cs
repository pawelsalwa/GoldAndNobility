using Common;
using Common.Fsm;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	/// <summary>
	/// opens up on loading state, i think dependancies are messed up since it has dependancy
	/// on both loading game state and scen loader service, think about it.
	/// </summary>
	internal class LoadingPanel : UiPanelBase
	{

		public GameObject loadingEndHint;
		public Image loadingBar;
		
		private IGameStates gameState;
		private ISceneLoader sceneLoader;
		private IGameFsm gameFsm;

		protected override void Awake()
		{
			base.Awake();
			
			gameState = ServiceLocator.RequestService<IGameStates>();
			gameState.Loading.OnEntered += OnLoadingStarted;
			gameState.LoadedAndWaiting.OnEntered += OnLoadingEnd;
			gameState.LoadedAndWaiting.OnExited += OnGameReady;
			
			sceneLoader = ServiceLocator.RequestService<ISceneLoader>();
			gameFsm = ServiceLocator.RequestService<IGameFsm>();
		}

		protected override void OnDestroy()
		{
			gameState.Loading.OnEntered -= OnLoadingStarted;
			gameState.LoadedAndWaiting.OnEntered -= OnLoadingEnd;
			gameState.LoadedAndWaiting.OnExited -= OnGameReady;
		}

		protected override void UpdateActive() 
		{
			loadingBar.fillAmount = sceneLoader.Progress;
			if (!sceneLoader.IsLoading && Input.anyKeyDown) gameFsm.EnterGame(); // xd
		}

		protected override void OnOpened()
		{
			loadingEndHint?.SetActive(false);
		}

		private void OnLoadingEnd() => loadingEndHint?.SetActive(true);

		private void OnGameReady() => Close();

		private void OnLoadingStarted() => Open();

	}
}
