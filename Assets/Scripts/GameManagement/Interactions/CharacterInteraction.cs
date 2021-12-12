using Common;
using DialogueSystem;
using InteractionSystem;

namespace GameManagement.Interactions
{
    internal class CharacterInteraction : InteractableBase
    {
        public DialogueData DialogueData;
        
        // public tradeoff
        
        // public Trade
        private IDialogueManager service;
        public static TradeEntity currentTrader;
        public TradeEntity tradeEntity;

        private void Start() => service = ServiceLocator.RequestService<IDialogueManager>();

        protected override void OnInteraction()
        {
            currentTrader = tradeEntity;
            service.OnDialogueEnded += ResetTraderObjectOnDialogueEnd;
            service.StartDialogue(DialogueData);

            void ResetTraderObjectOnDialogueEnd()
            {
                service.OnDialogueEnded -= ResetTraderObjectOnDialogueEnd;
                currentTrader = null;
            }
        }
    }
}
