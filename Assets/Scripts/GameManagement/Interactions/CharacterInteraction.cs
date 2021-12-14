using System.Linq;
using Common;
using DialogueSystem;
using InteractionSystem;
using UnityEngine;

namespace GameManagement.Interactions
{
    internal class CharacterInteraction : InteractableBase 
    {
        public DialogueData DialogueData;
        public TradeEntity tradeEntity;
        private IDialogueManager service;

        private void Start() => service = ServiceLocator.RequestService<IDialogueManager>();

        protected override void OnInteraction()
        {
            InitializeTradeEntity();
            service.StartDialogue(DialogueData);
        }

        /// <summary>
        /// if some quotes are dialogueActions, then we need to initialize them so they now with whom they trade :)
        /// </summary>
        private void InitializeTradeEntity()
        {
            var dialogueActionQuotes = DialogueData.quotes.Where(q => q.isDialogueAction);
            foreach (var q in dialogueActionQuotes)
                if (q.dialogueAction is TradingDialogueAction action)
                    action.tradeEntity = tradeEntity;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (!tradeEntity) tradeEntity = GetComponentInParent<TradeEntity>();
            if (!tradeEntity) tradeEntity = GetComponent<TradeEntity>();
            if (!tradeEntity)
            {
                Debug.Log($"<color=white>adding trade entity ;)</color>", gameObject);
                tradeEntity = gameObject.AddComponent<TradeEntity>();
            }
            
        }
    }
}
