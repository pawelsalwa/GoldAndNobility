using System;
using UnityEngine;

namespace CharacterSystem
{
	[Serializable]
	public class PlayerInput
	{
		public float LookAtAngle;
		public Vector2 Movement;
		public bool Attack;
		public bool Dodge;
		public bool Run;
		public bool shift;

		// public void ResetFields()
		// {
		// 	Attack = Dodge = Run = false;
		// 	Movement = Vector2.zero;
		// 	// LookAtAngle = 0;
		// }
	}
}