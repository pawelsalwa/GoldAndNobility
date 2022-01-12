using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace QuestSystem.Editor
{
	internal class QuestToolbar : Toolbar
	{
		private List<QuestData> QuestAssets;

		public QuestToolbar(QuestData data)
		{
			// var nodeCreateButton = new Button(OnCreateNodeButton) {text = "CreateNode"};
			// toolbar.Add(nodeCreateButton);

			QuestAssets = GetQuestAssets();
			var assetsLabels = QuestAssets.Select(x => x.name).ToList();
			var idx = QuestAssets.IndexOf(data);
			
			var dropdown = new DropdownField("Edited Quest: ", assetsLabels, idx); //, GetSelectedQuestName, GetQuestNameFromLabelName);
			dropdown.RegisterValueChangedCallback(DropdownChangedCallback);
			Add(dropdown);
		}

		private void DropdownChangedCallback(ChangeEvent<string> evt)
		{
			// Debug.Log($"<color=orange>dropdown change cb {evt.previousValue} to {evt.newValue}</color>");
			
			// selection change will rebuild whole graph :P
			Selection.objects = new Object[] { QuestAssets.FirstOrDefault(x => x.name == evt.newValue) }; 
		}

		private static List<QuestData> GetQuestAssets()
		{
			var filter = $"t:{typeof(QuestData)}";
			var guids = AssetDatabase.FindAssets(filter);
			var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
			var Quests = paths.Select(AssetDatabase.LoadAssetAtPath<QuestData>).ToList();
			return Quests;
		}

	}
}