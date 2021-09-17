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
			service.OnDialogueStarted += Open;
			service.OnDialogueEnded += Close;
			service.OnQuote += DisplayQuote;
			
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			service.OnDialogueStarted -= Open;
			service.OnDialogueEnded -= Close;
			service.OnQuote -= DisplayQuote;
		}

		private void DisplayQuote(Quote obj)
		{
			text.text = obj.talker + ": " + obj.text;
		}

	}
}