using System.Collections.Generic;
using Common;
using UnityEngine;

namespace InteractionSystem
{
	/// <summary> This should be attached to character or camera in game world to provide collider that can collide with interactable objects </summary>
	internal class InteractablesDetector : MonoBehaviour, IInteractablesProvider
	{

		public Transform CameraTransform => cameraAttached.transform;
		public List<Interactable> Interactables => interactables;
		
		public Camera cameraAttached;

		private readonly List<Interactable> interactables = new List<Interactable>();
		
		private InteractablesDetector() => ServiceLocator.RegisterService<IInteractablesProvider>(this);

		private void OnTriggerEnter(Collider other)
		{
			var interactable = other.GetComponent<Interactable>();
			if (!interactable)
			{
				Debug.Log($"<color=red> Game Object {other.gameObject} should have interactable on it or layer that doesnt allow interaction collision. </color>", other.gameObject);
				return;
			}

			interactable.OnDestroyed += OnDestroyed;
			interactables.Add(interactable);

			void OnDestroyed()
			{
				interactable.OnDestroyed -= OnDestroyed;
				OnTriggerExit(interactable.GetComponent<Collider>());
			}
		}

		private void OnTriggerExit(Collider other)
		{
			var interactable = other.GetComponent<Interactable>();
			if (!interactable)
			{
				Debug.Log($"<color=red> Game Object {other.gameObject} should have interactable on it or layer that doesnt allow interaction collision. </color>", other.gameObject);
				return;
			}

			interactables.Remove(interactable);
		}

		private void OnValidate()
		{
			if (cameraAttached) return;
			cameraAttached = GetComponentInParent<Camera>();
			if (cameraAttached) return;
			cameraAttached = Camera.main;
		}
	}
}