using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace InteractionSystem
{
	public abstract class Interactable : MonoBehaviour
	{
		// public InteractionType interactionType;
		//
		// [EnableIf(nameof(IsDialogue))] public DialogueData dialogue;
		// [EnableIf(nameof(IsPickable))] public Item item;

		[SerializeField] private Vector3 interactionTextLocalPos;

		public virtual string InteractionText => GetInteractionTxt();

		public Vector3 TextPosition => transform.position + interactionTextLocalPos;
		// private bool IsDialogue => interactionType == InteractionType.Dialogue;
		// private bool IsPickable => interactionType == InteractionType.Pickable;

		public void Interact() => OnInteraction();

		protected abstract void OnInteraction();

		private void OnValidate()
		{
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

		private string GetInteractionTxt()
		{
			// switch (interactionType)
			// {
			// 	case InteractionType.Dialogue: return "Talk";
			// 	case InteractionType.Pickable: return "Pick up";
			// }
			return "Dummy interaction";
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
}