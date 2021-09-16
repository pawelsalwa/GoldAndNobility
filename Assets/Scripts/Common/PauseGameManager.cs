using System;
using Common.GameInput;
using UnityEngine;

namespace Common
{
	
	// [PersistentComponent]
	public static class PauseGameManager
	{

		public static event Action OnPaused;
		public static event Action OnResumed;
		
		public static void Pause()
		{
			GameplayInput.enabled = false;
			Time.timeScale = 0f;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			OnPaused?.Invoke();
		}
		
		public static void Resume()
		{
			GameplayInput.enabled = true;
			Time.timeScale = 1f;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			OnResumed?.Invoke();
		}

	}
}