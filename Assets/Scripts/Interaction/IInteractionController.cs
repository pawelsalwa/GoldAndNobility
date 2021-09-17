namespace Interaction
{
	public interface IInteractionController
	{
		Interactable Current { get; }
		bool InteractionEnabled { set; }
	}
}