using System;
using System.Collections;
using Common.Attributes;
using Common.GameInput;
using NaughtyAttributes;
using UnityEngine;

namespace Common
{
	/// <summary>
	/// Schedules input switching for the end of frame
	/// so no different inputs are active during the same frame.
	/// </summary>
	[PersistentComponent]
	internal class InputSwitcher : MonoBehaviour, IInputSwitchService
	{
		private InputSwitcher() => ServiceLocator.RegisterService<IInputSwitchService>(this);

		[SerializeField, ReadOnly] private bool _gameplayInputEnabled;
		[SerializeField, ReadOnly] private bool _dialogueInputEnabled;
		[SerializeField, ReadOnly] private bool _uiInputEnabled;
		public InputFocus current { get; private set; }

		// public bool GameplayInputEnabled
		// {
		// 	set
		// 	{
		// 		_gameplayInputEnabled = value;
		// 		StopAllCoroutines();
		// 		StartCoroutine(ScheduleInputSwitch());
		// 	}
		// }
		//
		// public bool DialogueInputEnabled
		// {
		// 	set
		// 	{
		// 		_dialogueInputEnabled = value;
		// 		StopAllCoroutines();
		// 		StartCoroutine(ScheduleInputSwitch());
		// 	}
		// }
		//
		// public bool UiInputEnabled
		// {
		// 	set
		// 	{
		// 		_uiInputEnabled = value;
		// 		StopAllCoroutines();
		// 		StartCoroutine(ScheduleInputSwitch());
		// 	}
		// }

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

	public interface IInputSwitchService
	{
		// bool GameplayInputEnabled { set; }
		// bool DialogueInputEnabled { set; }
		// bool UiInputEnabled { set; }

		void SetInputFocus(InputFocus target);
		InputFocus current { get; }
	}

	public enum InputFocus { Gameplay, UI, Dialogue }
}