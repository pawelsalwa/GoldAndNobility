using System;
using Common.Attributes;
using Common.GameInput;
using UnityEngine;

namespace Common
{
	
	[PersistentComponent]
	public class PauseGameManager : MonoBehaviour
	{
		private static GameStateType stateCache;
		private static InputFocus focusCache;
		private static IInputSwitchService service;

		public static event Action OnPaused;
		public static event Action OnResumed;

		private void Awake()
		{
			service = ServiceLocator.RequestService<IInputSwitchService>();
		}

		private void Start()
		{
			if (GameState.Current == GameStateType.InGame)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				Debug.Log($"<color=white>setting cursor at start</color>");
			}
		}

		public static void Pause()
		{
			stateCache = GameState.Current; 
			GameState.Current = GameStateType.Paused;
			focusCache = service.current;
			service.SetInputFocus(InputFocus.UI);
			GameplayInput.enabled = false;
			Time.timeScale = 0f;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			OnPaused?.Invoke();
		}
		
		public static void Resume()
		{
			GameState.Current = stateCache;
			service.SetInputFocus(focusCache);
			GameplayInput.enabled = true;
			Time.timeScale = 1f;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			OnResumed?.Invoke();
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