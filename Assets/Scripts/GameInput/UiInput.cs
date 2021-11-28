using UnityEngine;

namespace GameInput
{
	public static class UiInput
	{
		private static bool _enabled;

		public static bool enabled { set => _enabled = value; }
		public static bool unpauseGame => _enabled && Input.GetKeyDown(KeyCode.Escape);
	}
} 