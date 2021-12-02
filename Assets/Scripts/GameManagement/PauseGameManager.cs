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
		public static event Action OnPaused;
		public static event Action OnResumed;

		private static bool paused = false;

		public static void Resume()
		{
			GameState.CancelState(GameStateType.Paused);
			paused = false;
			Time.timeScale = 1f;
			OnResumed?.Invoke();
		}

		private static void Pause()
		{
			GameState.ChangeState(GameStateType.Paused);
			paused = true;
			Time.timeScale = 0f;
			OnPaused?.Invoke();
		}

		private void Update()
		{
			if (paused && Input.GetKeyDown(KeyCode.Escape)) Resume();
			else if (Input.GetKeyDown(KeyCode.Escape)) Pause();
		}
	}
}