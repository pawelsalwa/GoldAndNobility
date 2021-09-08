using System.Reflection;
using UnityEngine;

namespace Editor
{
	internal static class EditorShortcuts
	{
		[UnityEditor.ShortcutManagement.Shortcut("ClearUnityConsole", KeyCode.X)]
		private static void ClearUnityConsole()
		{
			var assembly = Assembly.GetAssembly(typeof(UnityEditor.SceneView));
			var type = assembly.GetType("UnityEditor.LogEntries");
			var method = type.GetMethod("Clear");
			method.Invoke(new object(), null);
		}
	}
}