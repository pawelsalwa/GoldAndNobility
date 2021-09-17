using System;
using Common;
using Common.Attributes;
using Common.Fsm;
using UnityEngine;

namespace Tools
{
	[PersistentComponent]
	internal class DebugInfoPrinter : MonoBehaviour
	{
		public float x = 7;
		public float y = 35;
		public int fontSize = 25;
		
		// private IGameStates gameStates;
		private GUIStyle style = new GUIStyle();

		private static bool EnablePlaymodeTools
		{
			get => PlayerPrefs.GetInt("ShowOurFps") == 1;
			set => PlayerPrefs.SetInt("ShowOurFps", value ? 1 : 0);
		}

		private void Start()
		{
			// gameStates = ServiceLocator.RequestService<IGameStates>();
		}

		private void OnGUI()
		{
			if (!EnablePlaymodeTools) return;
			GUI.color = Color.green;
			GUI.Label(new Rect(x, y, 400f, 50f), $"GameState: {GameState.Current}", style);
			// GUI.Label(new Rect(x, y + 20, 400f, 50f), $"Input: {GameState.Current}", style);
		}

		private void OnValidate()
		{
			style.fontSize = fontSize;
			style.normal.textColor = Color.green;
		}
	}
}