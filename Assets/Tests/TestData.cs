using System.Collections.Generic;
using Dialogue;
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
				title = "helloTitle"
			},
			new Quote
			{
				talker = Talker.Player,
				text = "playerAnswer1",
				title = "playerAnswer1Title"
			},
			new Quote
			{
				talker = Talker.Player,
				text = "playerAnswer2",
				title = "playerAnswer2Title"
			},
			new Quote
			{
				talker = Talker.Npc,
				text = "bye",
				title = "byeTitle"
			},
		};

		newData.connections = new List<Connection> // connections connected so after 1st quote we have 2 player choices
		{
			new Connection(0, 1),
			new Connection(0, 2),
			new Connection(1, 3),
			new Connection(2, 3),
		};
		newData.entryQuote = newData.quotes[0];
		return newData;
	}
}