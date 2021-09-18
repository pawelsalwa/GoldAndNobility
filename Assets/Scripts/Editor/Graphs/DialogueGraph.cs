using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Graphs
{
	internal class DialogueGraph : EditorWindow
	{
		private DialogueGraphView graphView;

		[MenuItem("Graph/DialogueGraph")]
		private static void OpenWindow()
		{
			var window = GetWindow<DialogueGraph>();
			window.titleContent = new GUIContent("Dialogue Graph");
		}

		private void OnEnable()
		{
			ConstructGraphView();
			GenerateToolbar();
		}

		private void OnDisable()
		{
			rootVisualElement.Remove(graphView);
		}

		private void ConstructGraphView()
		{
			
			graphView = new DialogueGraphView { name = "Dialogue Graph"};
			graphView.StretchToParentSize();
			rootVisualElement.Add(graphView);
		}

		private void GenerateToolbar()
		{
			var toolbar = new Toolbar();
			var nodeCreateButton = new Button(CreateNode);
			nodeCreateButton.text = "CreateNode";
			toolbar.Add(nodeCreateButton);
			rootVisualElement.Add(toolbar);
		}

		private void CreateNode() => graphView.CreateNode("NewLine");
	}
}
