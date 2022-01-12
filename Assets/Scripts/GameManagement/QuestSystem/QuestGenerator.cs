using System.Collections.Generic;
using System.Linq;
using DialogueSystem;
using GameManagement.Interactions;
using UnityEngine;

namespace GameManagement.QuestSystem
{
    public class QuestGenerator : MonoBehaviour
    {
        public List<CharacterInteraction> characters;
        public CharacterInteraction startingCharacter;
        private DialogueData data;
     
        public List<Quote> questQuotes; 

        // [DidReloadScripts] private static void DidReloadScripts() => Debug.Log($"<color=white>didreloadScipts</color>");
        // [PostProcessSceneAttribute] private static void PostProcessSceneAttribute() => Debug.Log($"<color=white>PostProcessSceneAttribute</color>");

        private void Start()
        {
            // TypeCache.
            // characters = FindObjectsOfType<CharacterInteraction>().ToList();
            data = Instantiate(startingCharacter.DialogueData);
            startingCharacter.DialogueData = data;

            var lastConnectedQuote = data.entryQuote;
            foreach (var questQuote in questQuotes)
            {
                data.AddQuote(questQuote);
                data.AddEdge(lastConnectedQuote, questQuote);
                lastConnectedQuote = questQuote;
            }
            
            data.AddEdge(lastConnectedQuote, data.entryQuote);
        }
    }
}
