using UnityEngine;

namespace CharacterSystem
{
	public class Movement
	{
		
		private readonly CharacterController characterController;
		private readonly MovementSetup setup;
		private float targetRotY;

		/// <summary> This can be used for anim, its local space velocity (not affected by rotation) </summary>
		public Vector2 InternalCharacterVelocity { get; private set; }

		public Movement(CharacterController characterController, MovementSetup setup)
		{
			this.characterController = characterController;
			this.setup = setup;
		}

		public void FixedUpdate()
		{
			var eulerAngles = characterController.transform.eulerAngles; 
			var currentRotY = Mathf.LerpAngle(eulerAngles.y, targetRotY, setup.TurningFactor * Time.deltaTime);
			// Debug.Log($"<color=white>current: {angles}, new: {eulerAngles}</color>");
			eulerAngles = new Vector3(eulerAngles.x, currentRotY, eulerAngles.z);
			characterController.transform.eulerAngles = eulerAngles;
		}

		public void SetLookAtAngle(float lookAtAngle) => targetRotY = lookAtAngle;

		public void Update(PlayerInput input)
		{
			var inputMovement = input.Movement * (input.shift ? setup.RunSpeed : setup.WalkSpeed);
			InternalCharacterVelocity = inputMovement;
			Vector2 movement = Rotate(inputMovement, characterController.transform.eulerAngles.y);
			RequestControllerMove(movement);
			SetLookAtAngle(input.LookAtAngle);
		}

		private void RequestControllerMove(Vector2 motion)
		{
			Vector3 motion3d = new Vector3(motion.x, -Mathf.Abs(setup.forceDownSpeed), motion.y) * Time.deltaTime;
			characterController.Move(motion3d);
		}

		Vector2 Rotate(Vector2 aPoint, float aDegree)
		{
			float rad = aDegree * Mathf.Deg2Rad;
			float s = Mathf.Sin(rad);
			float c = Mathf.Cos(rad);
			return new Vector2(
				aPoint.x * c + aPoint.y * s,
				aPoint.y * c - aPoint.x * s);
		}

		// /// <summary> imeediately calls move on controller </summary>
		// public void ForceMoveForward(float value)
		// {
		// 	Vector2 movement = Rotate(Vector2.up * value, characterController.transform.eulerAngles.y);
		// 	characterController.Move(new Vector3(movement.x, 0, movement.y));
		// }
		//
		// public void OnAnimatorMove(Vector3 animatorDeltaPosition)
		// {
		// 	// if (animatorDeltaPosition.x > 0.01f || animatorDeltaPosition.z > 0.01f)
		// 		// Debug.Log($"<color=white>anim delta pos: x : {animatorDeltaPosition.x}, z: {animatorDeltaPosition.z} </color>");
		// 	// Vector2 movement = Rotate(new Vector2(animatorDeltaPosition.x, animatorDeltaPosition.z), characterController.transform.eulerAngles.y);
		// 	characterController.Move(new Vector3(animatorDeltaPosition.x, 0f, animatorDeltaPosition.z));
		// }
	}
}