using System;
using UnityEngine;

namespace Common.GameInput
{
	public static class GameplayInput
	{
		/// <summary> shouldnt be needed! but this way we turn off cinemachine orbit vcam </summary>
		public static event Action<bool> OnEnabledChanged;
		
		private static bool _enabled = true;

		public static bool enabled
		{
			get => _enabled;
			set
			{
				if (_enabled == value) return;
				_enabled = value;
				OnEnabledChanged?.Invoke(value);
			}
		}

		public static Vector2 movement => enabled ? new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) : Vector2.zero;

		public static bool up => enabled && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

		public static bool down => enabled && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));

		public static bool left => enabled && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));

		public static bool right => enabled && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));

		public static bool shiftDown => enabled && (Input.GetKeyDown(KeyCode.LeftShift));

		public static bool pauseGame => enabled && Input.GetKeyDown(KeyCode.Escape);

		public static bool interact => enabled && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse0));

	}
}