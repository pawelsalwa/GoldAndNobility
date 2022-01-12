using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace QuestSystem.Editor
{
	internal class QuestNode : Node
	{
		internal readonly QuestStage stage;

		private static readonly Vector2 defaultNodeSize = new(60, 150);
		private readonly bool entry;
		public Port inputPort { get; }

		internal QuestNode(QuestStage stage)
		{
			this.stage = stage;
			SetPosition(stage.pos);
			
			
			title = !string.IsNullOrEmpty(stage.text) ?
				stage.text.Substring(0, Mathf.Min(stage.text.Length, 17)) + "...":
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
				
				SetupQuestScriptableObjectField();
				// SetupTalkerEnumField();
				SetupTextField();
			}
			
			RefreshExpandedState();
			RefreshPorts();
		}

		private void SetupQuestScriptableObjectField()
		{
			var dialogueActionField = new ObjectField("Quest") {objectType = typeof(QuestStageBase)};
			dialogueActionField.SetValueWithoutNotify(stage.questStageBase);
			dialogueActionField.RegisterValueChangedCallback(OnChanged);
			mainContainer.Add(dialogueActionField);
		
			void OnChanged(ChangeEvent<Object> evt) => stage.questStageBase = evt.newValue as QuestStageBase;
		}


		// private void SetupTalkerEnumField()
		// {
		// 	var talkerEnumField = new EnumField("Talker", Talker.Npc);
		// 	talkerEnumField.RegisterValueChangedCallback(OnTalkerChanged);
		// 	talkerEnumField.SetValueWithoutNotify(stage.talker);
		// 	mainContainer.Add(talkerEnumField);
		// 	void OnTalkerChanged(ChangeEvent<Enum> evt) => stage.talker = (Talker)evt.newValue;
		// }

		private void SetupTextField()
		{
			var quoteTextField = new TextField();
			quoteTextField.RegisterValueChangedCallback(OnTextChanged);
			quoteTextField.SetValueWithoutNotify(stage.text);
			quoteTextField.multiline = true;
			mainContainer.Add(quoteTextField);
			void OnTextChanged(ChangeEvent<string> evt) => stage.text = evt.newValue;
		}

		private void OnAddPortBtn() => AddOutputPort();

		public override void UpdatePresenterPosition() => stage.pos = GetPosition();

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