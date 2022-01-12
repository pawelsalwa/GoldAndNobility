using UnityEditor;
using UnityEngine.UIElements;

namespace QuestSystem.Editor
{
	public class QuestWindow : EditorWindow
	{
		private QuestGraphView graphView;
		private QuestData editedData;
		private QuestToolbar toolbar;
		private bool initialized => editedData != null;

		public void Init(QuestData data)
		{
			editedData = data;
			EditorUtility.SetDirty(data);
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
			toolbar = new QuestToolbar(editedData);
			rootVisualElement.Add(toolbar);
		}

		private void ConstructGraphView()
		{
			graphView = new QuestGraphView(editedData);
			// graphView = new QuestGraphView(editedData);{name = "Quest Graph"};
			graphView.StretchToParentSize();
			rootVisualElement.Add(graphView);
		}
	}

}