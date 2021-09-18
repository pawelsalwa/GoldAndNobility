using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Graphs
{
	internal class DialogueNode : Node 
	{
		public string guid;

		public string dialogueText;

		public bool entryPoint;

		internal DialogueNode(bool entry = false)
		// internal DialogueNode(string title, string dialogueText, bool entryPoint = false)
		{
			title = entry ? "Start" : "New node";
			var button = new Button(AddPort) {text = "New choice"};
			titleContainer.Add(button);
			
			// var port = GeneratePort(node, Direction.Input);
			// port.portName = "input";
			//
			// node.outputContainer.Add(port);
			// node.RefreshExpandedState();
			// node.RefreshPorts();
			// node.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
			SetPosition(new Rect(100, 200, 100, 150));
			RefreshExpandedState();
			RefreshPorts();
		}

		private void AddPort()
		{
			var port = InstantiatePort(Orientation.Horizontal, Direction.Output,  Port.Capacity.Single, typeof(float));
			port.portName = "output";
			outputContainer.Add(port);
			RefreshExpandedState();
			RefreshPorts();
		}
	}
}
