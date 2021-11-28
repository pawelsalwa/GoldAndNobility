using UnityEditor;
using UnityEditor.Callbacks;

namespace Dialogue.Editor
{
	internal static class DialogueGraphWindowOpener
	{
		private const string windowName = "Dialogue Graph";

		[OnOpenAsset]
		private static bool OnDialogueAssetOpened(int instanceID)
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

		[MenuItem("Graph/DialogueGraph")]
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
			if (!EditorWindow.HasOpenInstances<DialogueGraphWindow>()) return;
			// if (!(EditorWindow.focusedWindow is DialogueGraphWindow)) return;
			InitWindow(data);
		}

		private static void InitWindow(DialogueData data)
		{
			var window = OpenWindow();
			window.Init(data);
		}
		
		private static DialogueGraphWindow OpenWindow() 
			=> EditorWindow.GetWindow<DialogueGraphWindow>(windowName);
	}
}