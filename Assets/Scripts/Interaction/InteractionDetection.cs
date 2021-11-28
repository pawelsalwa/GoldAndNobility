using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Interaction
{
	/// <summary> This should be attached to character or camera in game world to provide collider that can collide with interactable objects </summary>
	internal class InteractionDetection : MonoBehaviour, IInteractablesProvider
	{

		public List<Interactable> Interactables => interactables;
		
		private readonly List<Interactable> interactables = new List<Interactable>();
		
		private InteractionDetection() => ServiceLocator.RegisterService<IInteractablesProvider>(this);

		private void OnTriggerEnter(Collider other)
		{
			var interactable = other.GetComponent<Interactable>();
			if (!interactable)
			{
				Debug.Log($"<color=red> Game Object {other.gameObject} should have interactable on it or layer that doesnt allow interaction collision. </color>", other.gameObject);
				return;
			}

			interactables.Add(interactable);
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
	}
}