using System.Collections.Generic;

namespace Interaction
{
	internal interface IInteractablesProvider
	{
		List<Interactable> Interactables { get; }
	}
}