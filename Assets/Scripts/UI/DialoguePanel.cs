using Common;
using Common.GameInput;
using Dialogue;
using Interaction;
using TMPro;

namespace UI
{
	internal class DialoguePanel : UiPanelBase
	{
		public TextMeshProUGUI text;

		private IDialogueController service;

		protected override void OnOpened()
		{
			DisableInput();
			ServiceLocator.RequestService<IInteractionController>().InteractionEnabled = false;
		}

		protected override void OnClosed()
		{
			GameplayInput.enabled = true;
			ServiceLocator.RequestService<IInteractionController>().InteractionEnabled = true;
		}

		protected override void Awake()
		{
			base.Awake();

			service = ServiceLocator.RequestService<IDialogueController>();
			service.OnDialogueStarted += Open;
			service.OnDialogueEnded += Close;
			service.OnQuote += DisplayQuote;

			PauseGameManager.OnResumed += DisableInput;
		}

		/// <summary> not sure if this panel should take care of it, maybe we should handle pause during UI display somewhere else. </summary>
		private void DisableInput()
		{
			if (Active) GameplayInput.enabled = false;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			service.OnDialogueStarted -= Open;
			service.OnDialogueEnded -= Close;
			service.OnQuote -= DisplayQuote;

			PauseGameManager.OnResumed -= DisableInput;
		}

		private void DisplayQuote(Quote obj)
		{
			text.text = obj.talker + ": " + obj.text;
		}

	}
}