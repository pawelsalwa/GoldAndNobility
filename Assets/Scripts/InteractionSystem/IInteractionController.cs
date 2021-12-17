
using System;

namespace InteractionSystem
{
	public interface IInteractionController
	{
		bool TryInteract();
		void EnableInteraction();
		void DisableInteraction();
		internal void Init(IInteractablesProvider obj);
	}
}