using Dialogue;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.DialogueEditor
{
	internal class DialogueNode : Node
	{
		internal readonly Quote quote;

		private static readonly Vector2 defaultNodeSize = new Vector2(100, 150);
		private readonly bool entry;

		public override bool IsAscendable() { return base.IsAscendable(); }

		internal DialogueNode(Quote quote)
		{
			this.quote = quote;
			SetPosition(quote.pos);
			
			title = !string.IsNullOrEmpty(quote.title) ? quote.title : entry ? "Start" : "New node";

			if (entry)
			{
				AddOutputPort();
				capabilities = 0;
				// capabilities ^= Capabilities.Copiable & Capabilities.Deletable & Capabilities.Groupable & Capabilities.Droppable;
			}
			else
			{
				var button = new Button(AddOutputPort) {text = "New choice"};
				titleContainer.Add(button);
				AddInputPort();
			}

			// SetPosition(new Rect(entry ? 100 : 300, 200, defaultNodeSize.x, defaultNodeSize.y));
			RefreshExpandedState();
			RefreshPorts();
		}

		public override void UpdatePresenterPosition() => quote.pos = GetPosition();

		public sealed override void SetPosition(Rect newPos) => base.SetPosition(newPos); // virtual call in constructor.

		private void AddInputPort()
		{
			var portIn = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
			portIn.portName = "input";
			inputContainer.Add(portIn);
			RefreshExpandedState();
			RefreshPorts();
		}

		private void AddOutputPort()
		{
			var portOut = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
			portOut.portName = "output";
			outputContainer.Add(portOut);
			RefreshExpandedState();
			RefreshPorts();
		}
	}
}