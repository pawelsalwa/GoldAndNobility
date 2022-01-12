using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace DialogueSystem.Editor
{
	internal class DialogueNode : Node
	{
		internal readonly Quote quote;

		private static readonly Vector2 defaultNodeSize = new Vector2(100, 150);
		private readonly bool entry;
		public Port inputPort { get; }

		internal DialogueNode(Quote quote)
		{
			this.quote = quote;
			SetPosition(quote.pos);
			
			
			title = !string.IsNullOrEmpty(quote.text) ?
				quote.text.Substring(0, Mathf.Min(quote.text.Length, 17)) + "...":
				entry ? "Start" : "Default title :)";

			if (entry)
			{
				AddOutputPort();
				capabilities = 0; // capabilities ^= Capabilities.Copiable & Capabilities.Deletable & Capabilities.Groupable & Capabilities.Droppable;
			}
			else
			{
				var button = new Button(OnAddPortBtn) {text = "New choice"};
				titleContainer.Add(button);
				inputPort = AddInputPort();
				var ports = outputContainer.Query<Port>().ToList();
				if (ports.Count == 0) AddOutputPort(); // utiliy, so one optput port is by default
				
				SetupDialogueAction();
				SetupTalkerEnumField();
				SetupTextField();
			}
			
			RefreshExpandedState();
			RefreshPorts();
		}

		private void SetupDialogueAction()
		{
			var dialogueActionField = new ObjectField("DialogueAction") {objectType = typeof(DialogueAction)};
			dialogueActionField.SetValueWithoutNotify(quote.dialogueAction);
			dialogueActionField.RegisterValueChangedCallback(OnChanged);
			mainContainer.Add(dialogueActionField);

			// var toggle = new Toggle("Is dialogue action");
			// toggle.RegisterValueChangedCallback(OnChanged);
			// toggle.SetValueWithoutNotify(quote.isDialogueAction);
			// mainContainer.Add(toggle);
			void OnChanged(ChangeEvent<Object> evt) => quote.dialogueAction = evt.newValue as DialogueAction;
		}


		private void SetupTalkerEnumField()
		{
			var talkerEnumField = new EnumField("Talker", Talker.Npc);
			talkerEnumField.RegisterValueChangedCallback(OnTalkerChanged);
			talkerEnumField.SetValueWithoutNotify(quote.talker);
			mainContainer.Add(talkerEnumField);
			void OnTalkerChanged(ChangeEvent<Enum> evt) => quote.talker = (Talker)evt.newValue;
		}

		private void SetupTextField()
		{
			var quoteTextField = new TextField();
			quoteTextField.RegisterValueChangedCallback(OnTextChanged);
			quoteTextField.SetValueWithoutNotify(quote.text);
			quoteTextField.multiline = true;
			mainContainer.Add(quoteTextField);
			void OnTextChanged(ChangeEvent<string> evt) => quote.text = evt.newValue;
		}

		private void OnAddPortBtn() => AddOutputPort();

		public override void UpdatePresenterPosition() => quote.pos = GetPosition();

		public sealed override void SetPosition(Rect newPos) => base.SetPosition(newPos); // virtual call in constructor.

		private Port AddInputPort()
		{
			var portIn = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
			portIn.portName = "input";
			inputContainer.Add(portIn);
			RefreshExpandedState();
			RefreshPorts();
			return portIn;
		}

		public Port AddOutputPort()
		{
			var portOut = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
			portOut.portName = "output";
			outputContainer.Add(portOut);
			RefreshExpandedState();
			RefreshPorts();
			return portOut;
		}
	}
}