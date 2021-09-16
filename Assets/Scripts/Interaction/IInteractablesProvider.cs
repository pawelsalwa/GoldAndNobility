using System.Collections.Generic;

namespace Interaction
{
	public interface IInteractablesProvider
	{
		List<Interactable> Interactables { get; }
	}
}