﻿using UnityEngine;

namespace Common.GameInput
{
	public static class GameplayInput
	{

		public static bool enabled = true;

		public static Vector2 movement => enabled ? new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) : Vector2.zero;

		public static bool up => enabled && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

		public static bool down => enabled && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));

		public static bool left => enabled && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));

		public static bool right => enabled && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));

		public static bool shiftDown => enabled && (Input.GetKeyDown(KeyCode.LeftShift));

		public static bool pauseGame => enabled && Input.GetKeyDown(KeyCode.Escape);
		public static bool interact => enabled && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0));

		// private static bool interactUsed = false;
		// public static bool interact
		// {
		// 	get
		// 	{
		// 		if (interactUsed) return false;
		// 		var value = enabled && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse0));
		// 		return value;
		// 	}
		// }
		//
		// /// <summary> Marks key as used to make it return false until next frame </summary>
		// public static void UseInteractKey() => interactUsed = true;
		//
		// /// <summary> Restores uses for keys that are marked as 'used'. These can return true only once per frame.  </summary>
		// private void ResetUsedKeys()
		// {
		// 	interactUsed = false; 
		// }
		//
		// private void LateUpdate() => ResetUsedKeys();
	}
}