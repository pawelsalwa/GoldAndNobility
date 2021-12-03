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
		private static void OnMenuItem()
		{
			OpenWindow();
			if (Selection.objects.Length != 1) return;
			if (!(Selection.objects[0] is DialogueData data)) return;
			InitWindow(data);
		}

		private static void OnSelectionChanged()
		{
			if (Selection.objects.Length != 1) return;
			if (!(Selection.objects[0] is DialogueData data)) return;
			if (!EditorWindow.HasOpenInstances<DialogueWindow>()) return;
			// if (!(EditorWindow.focusedWindow is DialogueGraphWindow)) return;
			InitWindow(data);
		}

		private static void InitWindow(DialogueData data)
		{
			var window = OpenWindow();
			window.Init(data);
		}
		
		private static DialogueWindow OpenWindow() 
			=> EditorWindow.GetWindow<DialogueWindow>(windowName);
	}
}