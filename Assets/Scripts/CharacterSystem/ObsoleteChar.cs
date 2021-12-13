// using System;
// using UnityEngine;
//
// namespace Character
// {
// 	public class ObsoleteChar : MonoBehaviour
// 	{
//
// 		public CharacterController controller;
// 		private Vector3 InternalCharacterVelocity;
// 		[SerializeField] private float WalkSpeed = 4f;
// 		[SerializeField] private Camera camera;
//
// 		private void Update()
// 		{
// 			Vector3 movement = Vector3.zero;
// 			if (Input.GetKey(KeyCode.Space)) movement.y = 1f;
// 			if (Input.GetKey(KeyCode.LeftControl)) movement.y = -1f;
// 			if (Input.GetKey(KeyCode.W)) movement.x = 1f;
// 			if (Input.GetKey(KeyCode.S)) movement.x = -1f;
// 			if (Input.GetKey(KeyCode.A)) movement.z = -1f;
// 			if (Input.GetKey(KeyCode.D)) movement.z = 1f;
// 			MoveByInput(movement);
// 		}
// 		
// 		public void MoveByInput(Vector3 inputVal)
// 		{
// 			inputVal *= WalkSpeed;
// 			InternalCharacterVelocity = inputVal;
// 			var flatDir = Rotate(new Vector2(inputVal.x, inputVal.z), camera.transform.eulerAngles.y);
// 			Vector3 movement = new Vector3(flatDir.x, inputVal.y, flatDir.y);
// 			RequestControllerMove(movement);
// 		}
//
// 		private void RequestControllerMove(Vector2 motion)
// 		{
// 			Vector3 motion3d = new Vector3(motion.x, 0f, motion.y) * Time.deltaTime;
// 			controller.Move(motion3d);
// 		}
//
// 		Vector2 Rotate(Vector2 aPoint, float aDegree)
// 		{
// 			float rad = aDegree * Mathf.Deg2Rad;
// 			float s = Mathf.Sin(rad);
// 			float c = Mathf.Cos(rad);
// 			return new Vector2(
// 				aPoint.x * c + aPoint.y * s,
// 				aPoint.y * c - aPoint.x * s);
// 		}
// 	}
// }