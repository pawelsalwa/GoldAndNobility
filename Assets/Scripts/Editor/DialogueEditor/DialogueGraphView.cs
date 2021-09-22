using System.Collections.Generic;
using System.Linq;
using Dialogue;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
// using Edge = UnityEditor.Experimental.GraphView.Edge;

namespace Editor.DialogueEditor
{
	internal class DialogueGraphView : GraphView
	{
		private readonly DialogueData data;
		private List<DialogueData> dialogueAssets;

		internal DialogueGraphView(DialogueData data)
		{
			this.data = data;
			Setup();
			GenerateGraph();
		}

		public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
		{
			if (evt.target is DialogueGraphView || evt.target is Node)
				evt.menu.AppendAction("Create new", CreateFromContextMenu);

			base.BuildContextualMenu(evt);
		}

		public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) => ports.Where(port => port != startPort && startPort.node != port.node && startPort.direction != port.direction).ToList();

		private void GenerateGraph()
		{
			// if (data.quotes == null || data.connections == null) return;
			foreach (var quote in data.quotes) CreateNode(quote);
			foreach (var con in data.connections) CreateEdge(con);
		}

		private void CreateEdge(Connection connection)
		{
			var dialogueNodes = nodes.Cast<DialogueNode>().ToList();
			
			var outputNode = dialogueNodes.FirstOrDefault(IsOutputNode);
			var inputNode = dialogueNodes.FirstOrDefault(IsInputNode);

			var outputPort = outputNode.outputContainer.Q<Port>();
			if (outputPort == null) outputPort = outputNode.AddOutputPort();
			var inputPort = inputNode.inputPort;

			LinkEdge(outputPort, inputPort);
			
			bool IsOutputNode(DialogueNode node) => data.quotes.IndexOf(node.quote) == connection.outputIdx;
			bool IsInputNode(DialogueNode node) => data.quotes.IndexOf(node.quote) == connection.inputIdx;
		}

		private void LinkEdge(Port output, Port input)
		{
			var edge = new Edge {output = output, input = input};
			edge.input.Connect(edge);
			edge.output.Connect(edge);
			Add(edge);
		}

		private void Setup()
		{
			styleSheets.Add(Resources.Load<StyleSheet>("DialogueGraph"));
			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector()); 
			SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale + 0.5f);
			GenerateToolbar();
			GenerateGrid();
			GenerateMinimap();

			// elementsRemovedFromGroup += OnRemoved;
			graphViewChanged += OnGraphChange;
		}

		private GraphViewChange OnGraphChange(GraphViewChange change)
		{
			if (change.elementsToRemove != null) RemoveElements(change.elementsToRemove);
			if (change.edgesToCreate != null) CreateEdges(change.edgesToCreate);
			return change;
		}

		private void RemoveElements(List<GraphElement> elementsToRemove)
		{
			foreach (var e in elementsToRemove)
				if (e is DialogueNode node)
					data.quotes.Remove(node.quote);
				else if (e is Edge edge &&
				         edge.input.node is DialogueNode @in &&
				         edge.output.node is DialogueNode @out)
					data.RemoveEdge(@in.quote, @out.quote);
		}

		private void CreateEdges(List<Edge> edgesToCreate)
		{
			foreach (var edge in edgesToCreate)
				if (edge.output.node is DialogueNode @out &&
				    edge.input.node is DialogueNode @in)
					data.AddEdge(@out.quote, @in.quote);
		}

		private void GenerateMinimap()
		{
			var minimap = new MiniMap {anchored = true};
			minimap.SetPosition(new Rect(10, 30, 200, 140));
			Add(minimap);
		}

		private void GenerateGrid()
		{
			var grid = new GridBackground();
			Insert(0, grid);
			grid.StretchToParentSize();
		}

		private void GenerateToolbar()
		{
			var toolbar = new Toolbar();
			var nodeCreateButton = new Button(OnCreateNodeButton) {text = "CreateNode"};
			toolbar.Add(nodeCreateButton);

			dialogueAssets = GetDialogueAssets();
			var assetsLabels = dialogueAssets.Select(x => x.name).ToList();
			var idx = dialogueAssets.IndexOf(data);
			var dropdown = new DropdownField("Edited dialogue: ", assetsLabels, idx, GetSelectedDialogueName, GetDialogueNameFromLabelName);
			dropdown.RegisterValueChangedCallback(DropdownChangedCallback);
			
			toolbar.Add(dropdown);
			Add(toolbar);
		}

		private void DropdownChangedCallback(ChangeEvent<string> evt)
		{
			// Debug.Log($"<color=orange>dropdown change cb {evt.previousValue} to {evt.newValue}</color>");
			Selection.objects = new Object[] { dialogueAssets.FirstOrDefault(x => x.name == evt.newValue) }; // selection change will rebuild whole graph xP !!
		} 

		private string GetSelectedDialogueName(string arg)
		{
			// Debug.Log($"<color=yellow>GetSelectedDialogueName {arg}</color>");
			return arg;
		}

		private string GetDialogueNameFromLabelName(string arg)
		{
			// Debug.Log($"<color=yellow>GetDialogueNameFromLabelName {arg}</color>");
			return arg;
		}

		private static List<DialogueData> GetDialogueAssets()
		{
			var filter = $"t:{typeof(DialogueData)}";
			var guids = AssetDatabase.FindAssets(filter);
			var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
			var dialogues = paths.Select(AssetDatabase.LoadAssetAtPath<DialogueData>).ToList();
			return dialogues;
		}

		private void OnCreateNodeButton() => CreateNode(viewport.localBound.center);

		private void CreateFromContextMenu(DropdownMenuAction obj) => CreateNode(obj.eventInfo.mousePosition);

		private void CreateNode(Vector2 pos)
		{
			var quote = new Quote {pos = {min = pos}};
			data.quotes.Add(quote);
			CreateNode(quote);
		}

		private void CreateNode(Quote quote)
		{
			var node = new DialogueNode(quote);
			// node.AddOutputPort();
			AddElement(node);
		}
	}
}