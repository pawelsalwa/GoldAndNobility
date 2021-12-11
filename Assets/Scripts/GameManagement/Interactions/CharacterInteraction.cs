using Common;
using DialogueSystem;
using InteractionSystem;
using TradeSystem;

namespace GameManagement.Interactions
{
    internal class CharacterInteraction : Interactable
    {
        public DialogueData DialogueData;
        
        // public tradeoff
        
        // public Trade
        private IDialogueManager service;

        private void Start() => service = ServiceLocator.RequestService<IDialogueManager>();

        protected override void OnInteraction() => StartDialogue();

        private void StartDialogue() => service.StartDialogue(DialogueData, new TradeEntity());
    }
}
