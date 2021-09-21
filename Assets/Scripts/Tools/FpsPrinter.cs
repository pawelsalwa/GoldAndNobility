using System;
using System.Collections.Generic;
using Common;
using Common.Attributes;
using NaughtyAttributes;
using UnityEngine;

namespace Tools
{
	[PersistentComponent]
	internal class FpsPrinter : MonoBehaviour
	{
		private float currentFps;
		private List<float> fpsCache = new List<float>();
		private float avgFps;

		private int currentIdx = 0;
		private GUIStyle style;

		public float x = 7;
		public float y = 1;
		public int fontSize = 25;

		[SerializeField] private Optional<int> capFps;

		private static bool EnablePlaymodeTools
		{
			get => PlayerPrefs.GetInt("ShowOurFps") == 1;
			set => PlayerPrefs.SetInt("ShowOurFps", value ? 1 : 0);
		}

		public int framesAvgCount = 120;

		private void Awake()
		{
			style = new GUIStyle {normal = new GUIStyleState {textColor = Color.cyan}};
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.F1)) ToggleActive();
			if (Input.GetKey(KeyCode.Joystick1Button10) && Input.GetKeyDown(KeyCode.Joystick1Button11)) ToggleActive();
			if (!EnablePlaymodeTools) return;
			CheckTimeScale();
			Application.targetFrameRate = capFps.Enabled ? capFps.Value : 999;

			float current;

			if (Mathf.Approximately(Time.deltaTime, 0f) || framesAvgCount == 0) current = 0f;
			else current = 1f / Time.deltaTime / framesAvgCount;

			current = float.IsNaN(current) ? 0f : current; // can be nan if zero is somehow there
			avgFps += current;
			currentIdx++;
			if (currentIdx >= fpsCache.Count) currentIdx = 0;

			if (fpsCache.Count < framesAvgCount)
			{
				fpsCache.Add(current);
			}
			else
			{
				avgFps -= fpsCache[currentIdx];
				fpsCache[currentIdx] = current;
			}

			if (float.IsNaN(avgFps))
			{
				OnValidate();
			}
		}

		private static void CheckTimeScale()
		{
			if (Mathf.Approximately(Time.timeScale, 0f)) return;
			if (Input.GetKey(KeyCode.LeftShift)) Time.timeScale = 4f;
			else if (Input.GetKeyUp(KeyCode.LeftShift)) Time.timeScale = 1f;
			else if (Input.GetKey(KeyCode.LeftControl)) Time.timeScale = 0.3f;
			else if (Input.GetKeyUp(KeyCode.LeftControl)) Time.timeScale = 1f;
		}

#if UNITY_EDITOR
		[UnityEditor.ShortcutManagement.Shortcut("ToggleTools", KeyCode.F1)]
#endif
		private static void ToggleActive()
		{
			EnablePlaymodeTools = !EnablePlaymodeTools;
			Time.timeScale = 1f;
		}

		private void OnGUI()
		{
			if (!EnablePlaymodeTools) return;
			// GUI.color = Color.cyan;
			style.fontSize = fontSize;
			GUI.Label(new Rect(x, y, 400f, 50f), $"avgFps: {avgFps:0.}", style);
		}

		private void OnValidate()
		{
			fpsCache.Clear();
			avgFps = 0f;
		}

		[Button]
		private void DebugTest() => framesAvgCount = 0;
	}
}