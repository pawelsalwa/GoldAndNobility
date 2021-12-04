using System;
using Common;
using Common.Attributes;
using Common.Fsm;
using UnityEngine;

namespace Tools
{
	[GameService]
	internal class DebugInfoPrinter : MonoBehaviour
	{
		public float x = 7;
		public float y = 35;
		public int fontSize = 25;
		
		public GUIStyle style;
		private GameStateType currentState;

		private static bool EnablePlaymodeTools
		{
			get => PlayerPrefs.GetInt("EnablePlaymodeTools") == 1;
			set => PlayerPrefs.SetInt("EnablePlaymodeTools", value ? 1 : 0);
		}

		private void Start()
		{
			style = new GUIStyle
			{
				fontSize = fontSize,
				fontStyle = FontStyle.BoldAndItalic, 
				normal = new GUIStyleState {textColor = Color.green}
			};
			GameState.OnChanged += OnStateChanged;
		}

		private void OnDestroy()
		{
			GameState.OnChanged -= OnStateChanged;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.F1)) EnablePlaymodeTools = !EnablePlaymodeTools;
		}

		private void OnStateChanged(GameStateType obj) => currentState = obj;

		private void OnGUI()
		{
			if (!EnablePlaymodeTools) return;
			GUI.color = Color.green;
			GUI.Label(new Rect(x, y, 400f, 50f), $"GameState: {currentState}", style);
			// GUI.Label(new Rect(x, y + 20, 400f, 50f), $"Input: {GameState.Current}", style);
		}

		private void OnValidate()
		{
			if (style == null) return;
			style.fontSize = fontSize;
			style.normal.textColor = Color.green;
		}
	}
}