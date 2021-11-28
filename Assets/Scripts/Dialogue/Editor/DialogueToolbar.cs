using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogue.Editor
{
	internal class DialogueToolbar : Toolbar
	{
		private List<DialogueData> dialogueAssets;

		public DialogueToolbar(DialogueData data)
		{
			// var nodeCreateButton = new Button(OnCreateNodeButton) {text = "CreateNode"};
			// toolbar.Add(nodeCreateButton);

			dialogueAssets = GetDialogueAssets();
			var assetsLabels = dialogueAssets.Select(x => x.name).ToList();
			var idx = dialogueAssets.IndexOf(data);
			var dropdown = new DropdownField("Edited dialogue: ", assetsLabels, idx); //, GetSelectedDialogueName, GetDialogueNameFromLabelName);
			dropdown.RegisterValueChangedCallback(DropdownChangedCallback);
			Add(dropdown);
		}

		private void DropdownChangedCallback(ChangeEvent<string> evt)
		{
			// Debug.Log($"<color=orange>dropdown change cb {evt.previousValue} to {evt.newValue}</color>");
			
			// selection change will rebuild whole graph :P
			Selection.objects = new Object[] { dialogueAssets.FirstOrDefault(x => x.name == evt.newValue) }; 
		}

		private static List<DialogueData> GetDialogueAssets()
		{
			var filter = $"t:{typeof(DialogueData)}";
			var guids = AssetDatabase.FindAssets(filter);
			var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
			var dialogues = paths.Select(AssetDatabase.LoadAssetAtPath<DialogueData>).ToList();
			return dialogues;
		}

	}
}