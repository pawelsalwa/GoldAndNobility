using Common.Attributes;
using Common.Fsm;
using Common.GameInput;
using UnityEngine;

namespace Common
{
	/// <summary>
	/// pauses game when InGameState is active. then ui should handle unpausing
	/// </summary>
	[PersistentComponent]
	internal class GamePauser : MonoBehaviour
	{
		private IGameFsm gameFsm;
		private IGameStates gameStates;

		private void Start()
		{
			gameFsm = ServiceLocator.RequestService<IGameFsm>();
			gameStates = ServiceLocator.RequestService<IGameStates>();
			
			gameStates.InGame.OnEntered += Enable;
			gameStates.GamePaused.OnEntered += Disable;
		}

		private void OnDestroy()
		{
			gameStates.InGame.OnEntered -= Enable;
			gameStates.GamePaused.OnEntered -= Disable;
		}

		private void Enable() => enabled = true;

		private void Disable() => enabled = false;

		private void Update()
		{
			if (CharacterInput.pauseGame) gameFsm.PauseGame();
		}

	}
}