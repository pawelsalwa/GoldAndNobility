using Dialogue;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.DialogueEditor
{
	internal class DialogueGraphWindow : EditorWindow
	{
		private DialogueGraphView graphView;
		private DialogueData editedData;
		private bool initialized => editedData != null;

		public void Init(DialogueData data)
		{
			editedData = data;
			RebuildGraph();
		}

		private void RebuildGraph()
		{
			if (!initialized) return;
			ClearGraph();
			ConstructGraphView();
		}

		private void ClearGraph()
		{
			if (graphView != null) rootVisualElement.Remove(graphView);
		}

		private void OnEnable() => RebuildGraph();

		private void OnDisable() => ClearGraph();

		private void ConstructGraphView()
		{
			graphView = new DialogueGraphView(editedData);
			// graphView = new DialogueGraphView(editedData);{name = "Dialogue Graph"};
			graphView.StretchToParentSize();
			rootVisualElement.Add(graphView);
		}
	}
}