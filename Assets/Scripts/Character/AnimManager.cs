using UnityEngine;

namespace Character
{
	internal class AnimManager
	{
		private readonly Character character;
		private readonly Animator animator;
		private readonly AnimSetup setup;

		internal AnimManager(Character character)
		{
			this.character = character;
			animator = character.animator;
			setup = character.animSetup;
		}

		public void Update()
		{
			animator.SetFloat("x", character.Movement.InternalCharacterVelocity.x);
			animator.SetFloat("y", character.Movement.InternalCharacterVelocity.y);
		}

	}
}