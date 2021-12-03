using System.Collections.Generic;
using Common;
using DialogueSystem;
using TMPro;
using UnityEngine;

namespace UI
{
	internal class DialoguePanel : UiPanelBase
	{
		public TextMeshProUGUI text;
		public GameObject textDisplay;
		public DialogueChoices dialogueChoices;

		private IDialogueController service;

		protected override void Awake()
		{
			base.Awake();

			service = ServiceLocator.RequestService<IDialogueController>();
			service.OnDialogueStarted += OnDialogueStarted;
			service.OnDialogueEnded += Close;
			service.OnQuoteStarted += OnQuoteStarted;
			service.OnPlayerQuotesAppear += ShowPlayerQuotes;
			dialogueChoices.OnChoiceClicked += service.ChoosePlayerQuote;
		}

		private void OnQuoteStarted(Quote obj)
		{
			dialogueChoices.Hide();
			textDisplay.SetActive(!obj.isDialogueAction); // if quote is meant to be dialogue action, its not meant to be displayed (unity serializing abstract classes workaround)
			DisplayQuote(obj);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			service.OnDialogueStarted -= OnDialogueStarted;
			service.OnDialogueEnded -= Close;
			service.OnQuoteStarted -= DisplayQuote;
			dialogueChoices.OnChoiceClicked -= service.ChoosePlayerQuote;
		}

		private void OnDialogueStarted(DialogueData obj) => Open();

		private void DisplayQuote(Quote obj) => text.text = obj.talker + ": " + obj.text;

		private void ShowPlayerQuotes(List<Quote> quotes) => dialogueChoices.Show(quotes);
	}
}