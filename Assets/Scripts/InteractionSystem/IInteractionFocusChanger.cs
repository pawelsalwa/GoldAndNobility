using System;

namespace InteractionSystem
{
    internal interface IInteractionFocusChanger
    {
        /// <summary> focused interactable is the one that we can interact with currently </summary>
        event Action<InteractableBase> OnInteractableFocused;
    }
}