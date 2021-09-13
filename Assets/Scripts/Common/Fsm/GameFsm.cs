using System;
using Common.Attributes;
using Common.Fsm.States;
using UnityEngine;

namespace Common.Fsm
{
	[PersistentComponent]
	internal class GameFsm : MonoBehaviour, IGameFsm, IGameStates
	{
		private readonly IFsm fsm = new Fsm();

		public IState Current => fsm.CurrentState;
		public IState StartingApp { get; } = new StartingApp();
		public IState MainMenu { get; } = new MainMenu();
		public IState StartingNewGame { get; } = new StartingNewGame();
		public IState Loading { get; } = new Loading();
		public IState LoadedAndWaiting { get; } = new LoadedAndWaiting();
		public IState InGame { get; } = new InGame();
		public IState GamePaused { get; } = new GamePaused();

		public GameFsm()
		{
			ServiceLocator.RegisterService<IGameFsm>(this);
			ServiceLocator.RegisterService<IGameStates>(this);
		}

		private void Start()
		{
			StartApplication();
		}

		private void Awake()
		{
			// we should make sure not to do anything on awake, but rather in start, so other services can subscribe to states on awake
		}

		public void StartApplication() => RequestStateChange(StartingApp);
		public void EnterMainMenu() => RequestStateChange(MainMenu);
		public void StartNewGame() => RequestStateChange(StartingNewGame);
		public void EnterLoading() => RequestStateChange(Loading);
		public void FinishLoading() => RequestStateChange(LoadedAndWaiting);
		public void EnterGame() => RequestStateChange(InGame);
		public void PauseGame() => RequestStateChange(GamePaused);
		public void UnpauseGame() => RequestStateChange(InGame);

		private void RequestStateChange(IState target) => fsm.RequestStateChange(target);
		private void OnDestroy() => fsm?.CurrentState?.Exit();
	}
}