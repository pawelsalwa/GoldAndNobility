
namespace InteractionSystem
{
    public static class InteractionSystem
    {
        public static IInteractionController controller { get; internal set; }
        internal static IInteractionFocusChanger focusChanger;
    }
}