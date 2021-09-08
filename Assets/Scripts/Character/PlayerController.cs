using UnityEngine;

namespace Character
{
	public class PlayerController : Controller
	{

		public override void UpdateInput(PlayerInput playerInput)
		{
			var movement = new Vector2
			{
				x = Input.GetKey(KeyCode.D) ? 1f : Input.GetKey(KeyCode.A) ? -1f : 0f,
				y = Input.GetKey(KeyCode.W) ? 1f : Input.GetKey(KeyCode.S) ? -1f : 0f
			};

			playerInput.Movement = movement;
			playerInput.Attack = Input.GetKey(KeyCode.Mouse0);
			playerInput.Dodge = Input.GetKey(KeyCode.Space);
			playerInput.Run = Input.GetKeyDown(KeyCode.LeftShift);
			
			// playerInput.LookAtAngle = MainCamera.gameplayCamera.transform.rotation.eulerAngles.y; // for now get target rotation from cm orbit camera
		}
	}
}