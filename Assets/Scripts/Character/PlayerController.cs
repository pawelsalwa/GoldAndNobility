using Common.GameInput;
using UnityEngine;

namespace Character
{
	internal class PlayerController : MonoBehaviour
	{
		private Character chara;

		protected virtual void Start()
		{
			chara = GetComponent<Character>();
			if (!chara) enabled = false;
		}

		private void Update()
		{
			chara.input.Movement = CharacterInput.movement;
			chara.input.shift = CharacterInput.shiftDown;
			chara.input.LookAtAngle = Camera.main.transform.eulerAngles.y;
		}
	}
}