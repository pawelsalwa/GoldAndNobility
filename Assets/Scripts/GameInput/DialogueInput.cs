
using UnityEngine;

namespace GameInput
{
	public static class DialogueInput
	{
		internal static bool enabled = false;

		public static bool advanceDialogue => enabled && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E));
	}
}