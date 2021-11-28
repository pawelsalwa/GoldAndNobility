using System.Collections;
using Common;
using Common.Attributes;
using NaughtyAttributes;
using UnityEngine;

namespace GameInput
{
	/// <summary>
	/// Schedules input switching for the end of frame
	/// so no different inputs are active during the same frame.
	/// </summary>
	[PersistentComponent(typeof(IInputSwitchService))]
	internal class InputSwitcher : MonoBehaviour, IInputSwitchService
	{
		// private InputSwitcher() => ServiceLocator.RegisterService<IInputSwitchService>(this);

		[SerializeField, ReadOnly] private bool _gameplayInputEnabled;
		[SerializeField, ReadOnly] private bool _dialogueInputEnabled;
		[SerializeField, ReadOnly] private bool _uiInputEnabled;
		public InputFocus current { get; private set; }

		public void SetInputFocus(InputFocus target)
		{
			_dialogueInputEnabled = _gameplayInputEnabled = _uiInputEnabled = false;
			current = target;
			switch (target)
			{
				case InputFocus.Gameplay: _gameplayInputEnabled = true; break;
				case InputFocus.UI: _uiInputEnabled = true; break;
				case InputFocus.Dialogue: _dialogueInputEnabled = true; break;
			}
			StopAllCoroutines();
			StartCoroutine(ScheduleInputSwitch());
		}

		private IEnumerator ScheduleInputSwitch()
		{
			yield return new WaitForEndOfFrame();
			SetInputAtEndOfFrame();
		}

		private void SetInputAtEndOfFrame()
		{
			GameplayInput.enabled = _gameplayInputEnabled;
			DialogueInput.enabled = _dialogueInputEnabled;
			UiInput.enabled = _uiInputEnabled;
		}

	}

	public enum InputFocus { Gameplay, UI, Dialogue }
}