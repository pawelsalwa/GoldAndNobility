using System;
using Common;
using Common.Attributes;
using GameInput;
using UnityEngine;

namespace GameManagement
{
	
	[PersistentComponent]
	public class PauseGameManager : MonoBehaviour
	{
		private static GameStateType stateCache;
		private static InputFocus focusCache;
		private static IInputSwitchService service;

		public static event Action OnPaused;
		public static event Action OnResumed;

		public static void Resume()
		{
			GameState.Current = stateCache;
			service.SetInputFocus(focusCache);
			GameplayInput.enabled = true;
			Time.timeScale = 1f;
			OnResumed?.Invoke();
		}

		private static void Pause()
		{
			stateCache = GameState.Current; 
			GameState.Current = GameStateType.Paused;
			focusCache = service.current;
			service.SetInputFocus(InputFocus.UI);
			GameplayInput.enabled = false;
			Time.timeScale = 0f;
			OnPaused?.Invoke();
		}

		private void Start()
		{
			service = ServiceLocator.RequestService<IInputSwitchService>();
		}

		private void Update()
		{
			if (GameState.Current == GameStateType.Paused) HandleUnpause();
			else HandlePause();
		}

		private void HandlePause()
		{
			if (Input.GetKeyDown(KeyCode.Escape)) Pause();
		}

		private void HandleUnpause()
		{
			if (Input.GetKeyDown(KeyCode.Escape)) Resume();
		}
	}
}