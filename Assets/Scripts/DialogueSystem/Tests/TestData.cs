using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;

internal class TestData
{

	internal static DialogueData GetTestDialogueData()
	{
		var newData = ScriptableObject.CreateInstance<DialogueData>();
		newData.quotes = new List<Quote>
		{
			new Quote
			{
				talker = Talker.Npc,
				text = "hello",
			},
			new Quote
			{
				talker = Talker.Player,
				text = "playerAnswer1",
			},
			new Quote
			{
				talker = Talker.Player,
				text = "playerAnswer2",
			},
			new Quote
			{
				talker = Talker.Npc,
				text = "bye",
			},
		};

		newData.edges = new List<Edge> // connections connected so after 1st quote we have 2 player choices
		{
			new Edge(0, 1),
			new Edge(0, 2),
			new Edge(1, 3),
			new Edge(2, 3),
		};
		newData.entryQuote = newData.quotes[0];
		return newData;
	}
}