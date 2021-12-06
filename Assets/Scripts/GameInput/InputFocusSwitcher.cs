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
	[GameService]
	internal class InputFocusSwitcher : MonoBehaviour
	{
		private enum InputFocus { Gameplay, UI, Dialogue }
		
		[SerializeField, ReadOnly] private bool _gameplayInputEnabled;
		[SerializeField, ReadOnly] private bool _dialogueInputEnabled;
		[SerializeField, ReadOnly] private bool _uiInputEnabled;
		[SerializeField, ReadOnly] private InputFocus current;

		private void Start()
		{
			GameState.OnChanged += OnStateChanged;
			OnStateChanged(GameState.Current);
		}

		private void OnDestroy() => GameState.OnChanged -= OnStateChanged;

		private void OnStateChanged(GameStateType obj)
		{
			var type = InputFocus.Gameplay;
			switch (obj)
			{
				case GameStateType.None:
				case GameStateType.InGame:
					type = InputFocus.Gameplay;
					break;
				case GameStateType.MainMenu:
				case GameStateType.Loading:
				case GameStateType.Paused:
				case GameStateType.BrowsingInventory:
				case GameStateType.Trading:
				case GameStateType.AnnouncementTableBrowsing:
					type = InputFocus.UI;
					break;
				case GameStateType.InDialogue:
					type = InputFocus.Dialogue;
					break;
			}

			SetInputFocus(type);
		}

		private void SetInputFocus(InputFocus target)
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
}