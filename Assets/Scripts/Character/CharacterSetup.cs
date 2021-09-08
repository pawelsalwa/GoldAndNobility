using System;
using UnityEngine;

namespace Character
{
	[CreateAssetMenu(fileName = "CharacterSetup", menuName = "ScriptableObject/CharacterSetup")]
	public class CharacterSetup : ScriptableObject
	{

		public MovementSetup Movement;
	}
	
	[Serializable]
	public class MovementSetup
	{
		public float WalkSpeed = 2.5f;
		public float RunSpeed = 4.5f;
		public float TurningFactor = 0.3f;
		public float forceDownSpeed = 3f;
	}
}