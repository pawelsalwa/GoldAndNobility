using Cinemachine;
using UnityEngine;

namespace GameInput
{
	public static class GameplayInput
	{

		internal static bool enabled = true;
		
		public static Vector2 movement => enabled ? new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) : Vector2.zero;

		public static bool up => enabled && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

		public static bool down => enabled && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));

		public static bool left => enabled && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));

		public static bool right => enabled && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));

		public static bool shiftDown => enabled && (Input.GetKeyDown(KeyCode.LeftShift));

		public static bool pauseGame => enabled && Input.GetKeyDown(KeyCode.Escape);
		public static bool openInventory => enabled && (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab));
		public static bool interact => enabled && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0));

		static GameplayInput() => CinemachineCore.GetInputAxis = GetInputAxis;

		private static float GetInputAxis(string axisname) => enabled ? Input.GetAxis(axisname) : 0f;
	}
}