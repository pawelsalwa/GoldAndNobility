using DialogueSystem;
using UnityEditor;
using UnityEngine.UIElements;

namespace Dialogue.Editor
{
	internal class DialogueGraphWindow : EditorWindow
	{
		private DialogueGraphView graphView;
		private DialogueData editedData;
		private DialogueToolbar toolbar;
		private bool initialized => editedData != null;

		public void Init(DialogueData data)
		{
			editedData = data;
			RebuildGraph();
		}

		private void RebuildGraph()
		{
			ClearGraph();
			if (initialized) ConstructGraphView();
			GenerateToolbar();
		}

		private void ClearGraph()
		{
			if (rootVisualElement.Contains(graphView)) rootVisualElement.Remove(graphView);
		}
		
		private void OnFocus()
		{
			if (!initialized) ClearGraph();
			GenerateToolbar();
		}

		private void OnEnable() => RebuildGraph();

		private void OnDisable() => ClearGraph();
		
		private void GenerateToolbar()
		{
			if (rootVisualElement.Contains(toolbar)) rootVisualElement.Remove(toolbar);
			toolbar = new DialogueToolbar(editedData);
			rootVisualElement.Add(toolbar);
		}

		private void ConstructGraphView()
		{
			graphView = new DialogueGraphView(editedData);
			// graphView = new DialogueGraphView(editedData);{name = "Dialogue Graph"};
			graphView.StretchToParentSize();
			rootVisualElement.Add(graphView);
		}
	}
}