﻿using UnityEngine;

namespace Common.GameInput
{
	public static class CharacterInput
	{
		public static bool enabled = true;
		public static Vector2 movement => enabled ? new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) : Vector2.zero;

		public static bool up => enabled && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
		public static bool down => enabled && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));
		public static bool left => enabled && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
		public static bool right => enabled && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
		public static bool shiftDown => enabled && (Input.GetKeyDown(KeyCode.LeftShift));
		public static bool pauseGame => enabled && Input.GetKeyDown(KeyCode.Escape);
	}
}