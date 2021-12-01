using Common;
using Dialogue;
using InteractionSystem;
using UnityEngine;

namespace Useless
{
    public class DummyInteraction : Interactable
    {

        public DialogueData DialogueData;
        
        protected override void OnInteraction()
        {
            ServiceLocator.RequestService<IDialogueController>().StartDialogue(DialogueData);
        }
    }
}
