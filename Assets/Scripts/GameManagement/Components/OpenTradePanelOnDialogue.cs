using System;
using DialogueSystem;
using UnityEngine;

namespace GameManagement.Components
{
    public class OpenTradePanelOnDialogue : MonoBehaviour
    {
        public DialogueData dialogueData;
        public int quoteIdx;
        
        private void Start()
        {
            var quote = dialogueData.quotes[quoteIdx];
        }
    }
}
