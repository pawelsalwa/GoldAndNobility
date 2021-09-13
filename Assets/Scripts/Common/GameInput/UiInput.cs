using UnityEngine;

namespace Common.GameInput
{
	public static class UiInput
	{
		private static bool enabled;

		public static bool Enabled { set => enabled = value; }
		public static bool unpauseGame => enabled && Input.GetKeyDown(KeyCode.Escape);
	}
} 