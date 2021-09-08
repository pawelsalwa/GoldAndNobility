using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace Utils.Editor
{
	internal static class CopyTransformTool
	{
		private static Transform cachedTransform;
		
		[Shortcut("CopySelectedTransform", KeyCode.C, ShortcutModifiers.Alt)]
		private static void CopySelectedTransform()
		{
			cachedTransform = null;
			if (Selection.gameObjects.Length != 1) return;
			if (!Selection.gameObjects[0]) return;
			cachedTransform = Selection.gameObjects[0].transform;
		}
		
		[Shortcut("PasteSelectedTransform", KeyCode.V, ShortcutModifiers.Alt)]
		private static void PasteSelectedTransform()
		{
			if (!cachedTransform) return;
			if (Selection.gameObjects.Length == 0) return;
			foreach (var go in Selection.gameObjects)
			{
				if (!go) continue;
				go.transform.position = cachedTransform.position;
				go.transform.rotation = cachedTransform.rotation;
			}
		}
	}
}