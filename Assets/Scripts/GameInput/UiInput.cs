using UnityEngine;

namespace GameInput
{
	public static class UiInput
	{
		internal static bool enabled;

		public static bool unpauseGame => enabled && Input.GetKeyDown(KeyCode.Escape);
		public static bool closeInventory => enabled && Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I);
	}
} 