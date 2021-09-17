using Common;
using Dialogue;
using NaughtyAttributes;
using UnityEngine;

namespace Interaction
{
	public enum InteractionType {None, Dialogue, Barking,}

	public class Interactable : MonoBehaviour
	{
		public InteractionType interactionType;

		[EnableIf(nameof(Draw))]
		public DialogueData dialogue;

		private bool Draw => interactionType == InteractionType.Dialogue;

		public void Interact()
		{
			switch (interactionType)
			{
				case InteractionType.None: break;
				case InteractionType.Dialogue: ServiceLocator.RequestService<IDialogueController>().StartDialogue(dialogue); break;
				case InteractionType.Barking: break;
			}
		}

		private void OnValidate()
		{
			var xd = nameof(interactionType);
			if (LayerMask.LayerToName( gameObject.layer) == "Interactable" ) return;
			Debug.Log($"<color=red> Interactable component doesnt have Interactable layer! {gameObject}</color>", gameObject);
		}

	}
}