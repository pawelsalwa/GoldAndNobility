using System.Collections.Generic;
using Common;
using Dialogue;
using Interaction;
using TMPro;

namespace UI
{
	internal class DialoguePanel : UiPanelBase
	{
		public TextMeshProUGUI text;

		private IDialogueController service;

		protected override void OnOpened() => ServiceLocator.RequestService<IInteractionController>().InteractionEnabled = false;

		protected override void OnClosed() => ServiceLocator.RequestService<IInteractionController>().InteractionEnabled = true;

		protected override void Awake()
		{
			base.Awake();

			service = ServiceLocator.RequestService<IDialogueController>();
			service.OnDialogueStarted += OnDialogeuStarted;
			service.OnDialogueEnded += Close;
			service.OnQuoteStarted += DisplayQuote;
			service.OnPlayerChoicesAppear += ShowPlayerChoices;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			service.OnDialogueStarted -= OnDialogeuStarted;
			service.OnDialogueEnded -= Close;
			service.OnQuoteStarted -= DisplayQuote;
		}

		private void OnDialogeuStarted(DialogueData obj) => Open();

		private void DisplayQuote(Quote obj)
		{
			text.text = obj.talker + ": " + obj.text;
		}

		private void ShowPlayerChoices(List<Quote> quotes)
		{
			
		}

	}
}