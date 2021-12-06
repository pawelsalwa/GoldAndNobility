using UnityEditor;
using UnityEditor.Callbacks;

namespace DialogueSystem.Editor
{
	internal static class DialogueWindowOpener
	{
		private const string windowName = "Dialogue Graph";

		[OnOpenAsset]
		private static bool OnDialogueAssetOpened(int instanceID, int line)
		{
			var obj = EditorUtility.InstanceIDToObject(instanceID);
			var data = obj as DialogueData;
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

		[MenuItem("Window/DialogueEditor")]
		private static void OnMenuItem() => GetWindow(true);

		private static void OnSelectionChanged()
		{
			if (Selection.objects.Length != 1) return;
			if (!(Selection.objects[0] is DialogueData data)) return;
			if (!EditorWindow.HasOpenInstances<DialogueWindow>()) return;
			// if (!(EditorWindow.focusedWindow is DialogueWindow)) return;
			InitWindow(data);
		}

		private static void InitWindow(DialogueData data)
		{
			if (!data) return;
			if (!EditorWindow.HasOpenInstances<DialogueWindow>()) return;
			var window = GetWindow(false); // should keep window not on top
			window.Init(data);
		}
		
		private static DialogueWindow GetWindow(bool focus) => EditorWindow.GetWindow<DialogueWindow>(windowName, focus);
	}
}