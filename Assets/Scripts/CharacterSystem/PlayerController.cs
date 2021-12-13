using Common;
using GameInput;
using UnityEngine;

namespace CharacterSystem
{
	internal class PlayerController : MonoBehaviour
	{
		private Character chara;

		private void Awake()
		{
			GameState.ChangeState(GameStateType.InGame); // player controller shoyld be only one so it could kinda initialize that
		}

		protected virtual void Start()
		{
			chara = GetComponent<Character>();
			if (!chara) enabled = false;
		}

		private void Update()
		{
			chara.input.Movement = GameplayInput.movement;
			chara.input.shift = GameplayInput.shiftDown;
			chara.input.LookAtAngle = Camera.main.transform.eulerAngles.y;
		}
	}
}