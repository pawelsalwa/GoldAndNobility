using System.Linq;
using DialogueSystem;
using InteractionSystem;
using TradeSystem;
using UnityEngine;

namespace GameManagement.Interactions
{
    public class CharacterInteraction : InteractableBase 
    {
        public DialogueEntity dialogueEntity;
        public TradeEntity tradeEntity;

        protected override void OnInteraction()
        {
            InitializeTradeEntity();
            dialogueEntity.StartDialogue();
        }

        /// <summary>
        /// if some quotes are dialogueActions, then we need to initialize them so they now with whom they trade :)
        /// </summary>
        private void InitializeTradeEntity()
        {
            var dialogueActionQuotes = dialogueEntity.RuntimeDialogue.quotes.Where(q => q.isDialogueAction);
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
            
            if (!dialogueEntity) dialogueEntity = GetComponentInParent<DialogueEntity>();
            if (!dialogueEntity) dialogueEntity = GetComponent<DialogueEntity>();
            if (!dialogueEntity)
            {
                Debug.Log($"<color=white>adding dialogueEntity ;)</color>", gameObject);
                dialogueEntity = gameObject.AddComponent<DialogueEntity>();
            }
        }
    }
}
