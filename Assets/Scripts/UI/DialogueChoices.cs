using System;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary> TODO: refactor to pool choices instead of instantiate/destroy (not necessary tho as objects are created just during dialogue in small amount :))</summary>
    public class DialogueChoices : UIBehaviour
    {
        public event Action<Quote> OnChoiceClicked;
        [Header("Its inactive object reference - actual quotes will instantiate and activate next to it in hierarchy.")]
        public DialogueChoice choiceTemplate;
        public List<DialogueChoice> choices;

        public void Show(List<Quote> quotes)
        {
            RemoveAllChoices();
            foreach (var quote in quotes) 
                choices.Add(CreateChoice(quote));
        }

        public void Hide() => RemoveAllChoices();

        private DialogueChoice CreateChoice(Quote quote)
        {
            var newChoice = Instantiate(choiceTemplate, choiceTemplate.transform.parent, true);
            newChoice.Setup(quote.text, ChoiceClickedCallback); // TODO make sure theres no memory leak with this callback ;d
            return newChoice;
            
            void ChoiceClickedCallback() => OnChoiceClicked?.Invoke(quote);
        }

        private void RemoveAllChoices()
        {
            for (int i = choices.Count - 1; i >= 0; i--) Destroy(choices[i].gameObject);
            choices.Clear();
        } 

        protected override void OnValidate()
        {
            if (choiceTemplate && choiceTemplate.gameObject.activeSelf)
            {
                Debug.Log($"<color=orange>disabling template as it should be</color>");
                choiceTemplate.gameObject.SetActive(false);
            }
        }
    }
}