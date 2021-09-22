using System.Collections.Generic;
using System.Linq;
using Dialogue;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.DialogueEditor
{
	internal class DialogueGraphView : GraphView
	{
		private readonly DialogueData data;
		private Port startPort;

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

		public override List<Port> GetCompatiblePorts(Port port, NodeAdapter nodeAdapter) => ports.Where(p => p != port && port.node != p.node && port.direction != p.direction).ToList();

		private void GenerateGraph()
		{
			foreach (var quote in data.quotes) CreateNode(quote);
			foreach (var con in data.connections) CreateEdge(con);
			GenerateEntryEdge();
		}

		private void GenerateEntryEdge()
		{
			var dialogueStartNode = 
				this.Query<DialogueNode>()
					.ToList()
					.FirstOrDefault(n => n.quote == data.entryQuote);
			
			if (dialogueStartNode == null) return;
			LinkEdge(startPort, dialogueStartNode.inputPort);
		}

		private void CreateEdge(Connection connection)
		{
			var dialogueNodes = nodes.Where(n => n is DialogueNode).Cast<DialogueNode>();
			
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
			GenerateStartNode();
			// elementsRemovedFromGroup += OnRemoved;
			graphViewChanged += OnGraphChange;
		}

		private void GenerateStartNode()
		{
			var startNode = new Node {capabilities = 0};
			startPort = startNode.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
			startPort.portName = "Start";
			startNode.outputContainer.Add(startPort);
			startNode.SetPosition(new Rect(100, 300, 100, 100));
			startNode.RefreshPorts();
			startNode.RefreshExpandedState();
			AddElement(startNode);
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
				else if (edge.output == startPort) data.entryQuote = (edge.input.node as DialogueNode).quote;
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

		private void GenerateToolbar() => Add(new DialogueToolbar(data));

		private void OnCreateNodeButton() => CreateNode(viewport.localBound.center);

		private void CreateFromContextMenu(DropdownMenuAction obj) => CreateNode(obj.eventInfo.mousePosition);

		private void CreateNode(Vector2 pos)
		{
			var quote = new Quote {pos = {min = pos}};
			data.quotes.Add(quote);
			CreateNode(quote);
		}

		private void CreateNode(Quote quote) => AddElement(new DialogueNode(quote));
	}
}