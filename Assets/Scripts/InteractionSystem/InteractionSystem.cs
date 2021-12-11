using UnityEngine;

namespace InteractionSystem
{
    public static class InteractionSystem
    {
        public static IInteractionController controller;
        internal static IInteractionFocusChanger focusChanger;
        private static InteractionController internalController;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateControllerObject()
        {
            internalController = new GameObject("[InteractionSystem.Controller]").AddComponent<InteractionController>();
            controller = internalController;
            focusChanger = internalController;
            Object.DontDestroyOnLoad(internalController);
        }

        internal static void InitializeDetector(InteractablesDetector detector) => internalController.Init(detector);
    }
}