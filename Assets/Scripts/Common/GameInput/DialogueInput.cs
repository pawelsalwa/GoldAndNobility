﻿
using UnityEngine;

namespace Common.GameInput
{
	public static class DialogueInput
	{
		public static bool enabled = false;

		public static bool advanceDialogue => enabled && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return));
	}
}