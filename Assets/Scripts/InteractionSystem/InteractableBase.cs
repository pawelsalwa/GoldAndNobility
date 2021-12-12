using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace InteractionSystem
{
	public abstract class InteractableBase : MonoBehaviour, IInteractable
	{
		public event Action OnDestroyed;

		[SerializeField] private Vector3 interactionTextLocalPos;

		public virtual string InteractionText => "interact";

		public Vector3 TextPosition => transform.position + interactionTextLocalPos;

		public void Interact() => OnInteraction();

		protected abstract void OnInteraction();

		private void OnDestroy() => OnDestroyed?.Invoke();

		private void OnValidate()
		{
			return;
			if (LayerMask.LayerToName(gameObject.layer) != "Interactable")
			{
				Debug.Log($"<color=red> Interactable component {gameObject} doesnt have Interactable layer! Fixing... </color>", gameObject);
				gameObject.layer = LayerMask.NameToLayer("Interactable");
			}

			var col = GetComponent<Collider>();
			if (!col)
			{
				Debug.Log($"<color=red>Interactable doesnt have trigger on it, adding sphere collider... {gameObject}</color>", gameObject);
				col = gameObject.AddComponent<SphereCollider>();
			}
			col.isTrigger = true;
		}

		private void OnDrawGizmosSelected()
		{
#if UNITY_EDITOR
			var cache = Handles.color;
			Handles.color = Color.magenta;
			Handles.Label( transform.position + interactionTextLocalPos, "Interaction text");
			Handles.color = cache;
#endif
		}
	}

	public interface IInteractable
	{
		void Interact();
	}
}