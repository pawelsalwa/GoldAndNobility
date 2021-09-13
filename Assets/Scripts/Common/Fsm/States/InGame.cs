using Common.GameInput;
using UnityEngine;

namespace Common.Fsm.States
{
	internal class InGame : StateBase
	{
		protected override void OnEnter()
		{
			CharacterInput.Enabled = true;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		protected override void OnExit()
		{
			CharacterInput.Enabled = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}