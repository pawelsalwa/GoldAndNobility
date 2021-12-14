using System;
using UnityEngine;

namespace Useless
{
	public class FlyCam : MonoBehaviour
	{

		public float cameraSensitivity = 90;
		public float climbSpeed = 4;
		public float normalMoveSpeed = 10;
		public float slowMoveFactor = 0.25f;
		public float fastMoveFactor = 3;

		private float rotationX = 0.0f;
		private float rotationY = 0.0f;

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		void Update ()
		{
			rotationX += Input.GetAxis("MouseX") * cameraSensitivity * Time.deltaTime;
			rotationY += Input.GetAxis("MouseY") * cameraSensitivity * Time.deltaTime;
			rotationY = Mathf.Clamp (rotationY, -90, 90);

			transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

			if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
			{
				transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
				transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
			}
			else if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl))
			{
				transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
				transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
			}
			else
			{
				transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
				transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
			}

			if (Input.GetKey (KeyCode.Space)) {transform.position += Vector3.up * climbSpeed * Time.deltaTime;}
			if (Input.GetKey (KeyCode.C) || Input.GetKey(KeyCode.LeftControl)) {transform.position -= Vector3.up * climbSpeed * Time.deltaTime;}

			// if (Input.GetKeyDown (KeyCode.End))
			// {
			// 	Screen.lockCursor = (Screen.lockCursor == false) ? true : false;
			// }
		}
	}
}