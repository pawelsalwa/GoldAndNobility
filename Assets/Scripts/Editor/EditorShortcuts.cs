using System;
using System.Reflection;
using Common.Attributes;
using UnityEngine;

namespace Editor
{
	internal static class EditorShortcuts
	{
		[UnityEditor.ShortcutManagement.Shortcut("ClearUnityConsole", KeyCode.X)]
		internal static void ClearUnityConsole()
		{
			var assembly = Assembly.GetAssembly(typeof(UnityEditor.SceneView));
			var type = assembly.GetType("UnityEditor.LogEntries");
			var method = type.GetMethod("Clear");
			method.Invoke(new object(), null);
		}
	}

	[GameService]
	internal class ClearConsoleEditorTool : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.X)) EditorShortcuts.ClearUnityConsole();
		}
	}
}