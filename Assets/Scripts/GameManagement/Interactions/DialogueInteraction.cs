using Common;
using DialogueSystem;
using InteractionSystem;

namespace GameManagement.Interactions
{
    internal class DialogueInteraction : Interactable
    {
        public DialogueData DialogueData;
        private IDialogueController service;

        private void Start()
        {
            service = ServiceLocator.RequestService<IDialogueController>();
        }

        protected override void OnInteraction() => StartDialogue();

        private void StartDialogue() => service.StartDialogue(DialogueData);
    }
}
