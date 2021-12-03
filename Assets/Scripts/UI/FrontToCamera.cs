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
			if (!cam) cam = Camera.main;
			if (!cam) return;
			transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
				cam.transform.rotation * Vector3.up);
		}

	}
}