using System;
using UnityEngine;

namespace UI
{
	public class FrontToCamera : MonoBehaviour
	{
		Camera cam;

		private void Start() => cam = Camera.main;

		public void Update()
		{
			if (cam == null) cam = Camera.main;
			if (cam == null) return;
			transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
				cam.transform.rotation * Vector3.up);
		}

	}
}