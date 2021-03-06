using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace QuestSystem.Editor
{
	internal static class QuestWindowOpener
	{
		private const string windowName = "Quest Graph";

		[OnOpenAsset]
		private static bool OnQuestAssetOpened(int instanceID, int line)
		{
			var obj = EditorUtility.InstanceIDToObject(instanceID);
			var data = obj as Quest;
			if (!data) return false;
			GetWindow(true);
			InitWindow(data);
			return true; // means - we handled opening
		}

		[InitializeOnLoadMethod]
		private static void Init()
		{
			Selection.selectionChanged -= OnSelectionChanged;
			Selection.selectionChanged += OnSelectionChanged;
		}

		[MenuItem("Window/QuestEditor")]
		private static void OnMenuItem() => GetWindow(true);

		private static void OnSelectionChanged()
		{
			if (Selection.objects.Length != 1) return;
			if (!(Selection.objects[0] is GameObject go)) return;
			var data = go.GetComponent<Quest>();
			if (!data) return;
			if (!EditorWindow.HasOpenInstances<QuestWindow>()) return;
			// if (!(EditorWindow.focusedWindow is QuestWindow)) return;
			InitWindow(data);
		}

		internal static void InitWindow(Quest data)
		{
			if (!data) return;
			if (!EditorWindow.HasOpenInstances<QuestWindow>()) return;
			var window = GetWindow(false); // should keep window not on top
			window.Init(data);
		}
		
		private static QuestWindow GetWindow(bool focus) => EditorWindow.GetWindow<QuestWindow>(windowName, focus);
	}
}