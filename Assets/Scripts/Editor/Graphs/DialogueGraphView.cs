using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Graphs
{
	internal class DialogueGraphView : GraphView
	{

		private static readonly Vector2 defaultNodeSize = new Vector2(100, 150);


		internal DialogueGraphView()
		{
			styleSheets.Add(Resources.Load<StyleSheet>("DialogueGraph"));
			
			SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale + 0.5f);
			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector());

			var grid = new GridBackground();
			Insert(0, grid);
			grid.StretchToParentSize();

			var tex = new Texture2D(1, 1);
			tex.SetPixel(0,0, Color.magenta );
			tex.Apply();
			// var img = new Image() {image = tex};
			// img.
			// Add();
			AddElement(GenerateEntryPointNode());
		}

		public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
		{
			return ports.Where(p => p != startPort && startPort.node != p.node).ToList();
			
			// var compatiblePorts = new List<Port>();
			// ports.ForEach(port =>
			// {
			// 	if (startPort != port && startPort.node != port.node) compatiblePorts.Add(port);
			// });
			// return compatiblePorts;
			return base.GetCompatiblePorts(startPort, nodeAdapter);
		}

		private Port GeneratePort(DialogueNode node, Direction dir, Port.Capacity capacity = Port.Capacity.Single)
		{
			return node.InstantiatePort(Orientation.Horizontal, dir,  capacity, typeof(float)); // arbitrary type? why i dont know
		}

		private DialogueNode GenerateEntryPointNode()
		{
			var node = new DialogueNode(true);
				
			var port = GeneratePort(node, Direction.Output);
			port.portName = "Next";
			node.outputContainer.Add(port);

			node.RefreshExpandedState();
			node.RefreshPorts();

			node.SetPosition(new Rect(100, 200, 100, 150));
			return node;
		}

		public void CreateNode(string nodeName) => AddElement(new DialogueNode());
		// public void CreateNode(string nodeName) => AddElement(CreateDialogueNode(nodeName));

		// private DialogueNode CreateDialogueNode(string title)
		// {
		// 	var node = new DialogueNode(title, "dialogueText");
		//
		// 	var button = new Button(() => OnAddButton(node)) {text = "New choice"};
		// 	node.titleContainer.Add(button);
		// 	
		// 	var port = GeneratePort(node, Direction.Input);
		// 	port.portName = "input";
		// 	
		// 	node.outputContainer.Add(port);
		// 	node.RefreshExpandedState();
		// 	node.RefreshPorts();
		// 	node.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
		// 	return node;
		// }

		// private void OnAddButton(DialogueNode node)
		// {
		// 	var port = GeneratePort(node, Direction.Output);
		//
		// 	var outputPortCount = node.outputContainer.Query("connector").ToList().Count;
		// 	port.portName = $"Choice {outputPortCount}";
		// 	
		// 	var deleteButton = new Button(() => { }) {text = "x"};
		// 	port.contentContainer.Add(deleteButton);
		// 	
		// 	node.outputContainer.Add(port);
		// 	node.RefreshPorts();
		// 	node.RefreshExpandedState();
		// }
	}
}