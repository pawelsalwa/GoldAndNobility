using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
	public class DialogueUI : MonoBehaviour
	{

		public TextMeshProUGUI text;
		public GameObject textDisplay;
		public DialogueChoices dialogueChoices;
		private IDialogueController controller;

		private void Start()
		{
			controller = DialogueController.instance;
			
			controller.OnQuoteStarted += OnQuoteStarted;
			controller.OnPlayerQuotesAppear += ShowPlayerQuotes;
			dialogueChoices.OnChoiceClicked += controller.ChoosePlayerQuote;
		}

		private void OnDestroy()
		{
			controller.OnQuoteStarted -= OnQuoteStarted;
			controller.OnPlayerQuotesAppear -= ShowPlayerQuotes;
			dialogueChoices.OnChoiceClicked -= controller.ChoosePlayerQuote;
		}

		private void OnQuoteStarted(Quote obj)
		{
			dialogueChoices.Hide();
			textDisplay.SetActive(!obj.isDialogueAction); // if quote is meant to be dialogue action, its not meant to be displayed (unity serializing abstract classes workaround)
			DisplayQuote(obj);
		}
		
		private void DisplayQuote(Quote obj) => text.text = obj.talker + ": " + obj.text;

		private void ShowPlayerQuotes(List<Quote> quotes) => dialogueChoices.Show(quotes);
	}
}