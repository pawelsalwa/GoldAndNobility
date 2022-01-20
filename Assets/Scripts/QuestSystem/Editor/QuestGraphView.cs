using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace QuestSystem.Editor
{
	internal class QuestGraphView : GraphView
	{
		private readonly Quest data;
		private Port startPort;

		internal QuestGraphView(Quest data)
		{
			this.data = data;
			Setup();
			GenerateGraph();
		}

		public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
		{
			if (evt.target is QuestGraphView || evt.target is Node)
			{
				var questTypes = UnityEditor.TypeCache.GetTypesDerivedFrom<QuestStage>();
				foreach (var questType in questTypes)
				{
					evt.menu.AppendAction($"Create Quest.../{questType.Name}", CreateFromContextMenu, ActionStatusCallack, questType);
				}
			}

			base.BuildContextualMenu(evt);
		}

		private DropdownMenuAction.Status ActionStatusCallack(DropdownMenuAction arg)
		{
			return DropdownMenuAction.Status.Normal;
		}

		public override List<Port> GetCompatiblePorts(Port port, NodeAdapter nodeAdapter) => ports.Where(p => p != port && port.node != p.node && port.direction != p.direction).ToList();

		private void GenerateGraph()
		{
			foreach (var stage in data.stages) CreateNode(stage);
			foreach (var con in data.connections) CreateEdge(con);
			GenerateEntryEdge();
		}

		private void GenerateEntryEdge()
		{
			var QuestStartNode = 
				this.Query<QuestNode>()
					.ToList()
					.FirstOrDefault(n => n.stage == data.entryStage);
			
			if (QuestStartNode == null) return;
			LinkEdge(startPort, QuestStartNode.inputPort);
		}

		private void CreateEdge(Connection connection)
		{
			// var QuestNodes = nodes.ToList().OfType<QuestNode>().ToList();
			// var QuestNodes = nodes.ToList().Cast<QuestNode>().ToList();
			var QuestNodes = nodes.Where(n => n is QuestNode).Cast<QuestNode>().ToList(); // api changed x(
			
			var outputNode = QuestNodes.FirstOrDefault(IsOutputNode);
			var inputNode = QuestNodes.FirstOrDefault(IsInputNode);

			var outputPort = outputNode.outputContainer.Q<Port>();
			if (outputPort.connections.Count() != 0)
				outputPort = outputNode.AddOutputPort(); // we need to add output ports each single time :)
			
			// outputNode.outputContainer.Remove(outputPort); //... theres always one port by default so lets remove it so line below works :)
			// if (outputPort == null) outputPort = outputNode.AddOutputPort();
			var inputPort = inputNode.inputPort;

			LinkEdge(outputPort, inputPort);
			
			bool IsOutputNode(QuestNode node) => data.stages.IndexOf(node.stage) == connection.outputIdx;
			bool IsInputNode(QuestNode node) => data.stages.IndexOf(node.stage) == connection.inputIdx;
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
			styleSheets.Add(Resources.Load<StyleSheet>("QuestGraph"));
			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector()); 
			SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale + 0.5f);
			// GenerateToolbar();
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
				if (e is QuestNode node)
					data.RemoveStage(node.stage);
				else if (e is Edge edge &&
				         edge.input.node is QuestNode @in &&
				         edge.output.node is QuestNode @out)
					data.RemoveEdge(@out.stage, @in.stage);
		}

		private void CreateEdges(List<Edge> edgesToCreate)
		{
			foreach (var edge in edgesToCreate)
				if (edge.output.node is QuestNode @out &&
				    edge.input.node is QuestNode @in)
					data.AddEdge(@out.stage, @in.stage);
				else if (edge.output == startPort) data.entryStage = (edge.input.node as QuestNode).stage;
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

		// private void GenerateToolbar() => Add(new QuestToolbar(data));

		// private void OnCreateNodeButton() => CreateNode(viewport.localBound.center);

		private void CreateFromContextMenu(DropdownMenuAction obj)
		{
			var questType = (Type) obj.userData;
			if (!questType.IsSubclassOf(typeof(QuestStage))) throw new ArgumentException($"trying to add quest stage of type {questType.Name}, it should derive from {nameof(QuestStage)}");
			var stage = data.AddStage(questType);
			stage.pos = obj.eventInfo.mousePosition;
			CreateNode(stage);
		}

		private void CreateNode(QuestStage stage) => AddElement(new QuestNode(stage));
	}
}