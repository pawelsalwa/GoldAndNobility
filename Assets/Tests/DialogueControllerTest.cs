using System.Collections.Generic;
using Dialogue;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
	internal class DialogueControllerTest
	{
		private DialogueController controller;
		private DialogueData data;

		[SetUp]
		public void Setup()
		{
			controller = new GameObject("test").AddComponent<DialogueController>();
			data = GetTestData();
		}

		[Test]
		public void StartData()
		{
			controller.StartDialogue(data);
			Assert.AreEqual(controller.CurrentDialogue, data);
		}
		
		[Test]
		public void DialogueStartedEvent()
		{
			DialogueData currentData = null;
			controller.OnDialogueStarted += OnDialogueStarted;
			controller.StartDialogue(data);
			controller.OnDialogueStarted -= OnDialogueStarted;
			Assert.That(currentData == data);

			void OnDialogueStarted(DialogueData d) => currentData = d;
		}

		[Test]
		public void QuoteStartedEvent()
		{
			Quote saidQuote = null;
			controller.OnQuoteStarted += OnQuoteStarted;
			controller.StartDialogue(data);
			Assert.That(saidQuote == data.quotes[0]);
			controller.Skip();
			Assert.That(saidQuote == data.quotes[1]);
			controller.OnQuoteStarted -= OnQuoteStarted;

			void OnQuoteStarted(Quote q) => saidQuote = q;
		}

		[Test]
		public void PassWholeDiaologue()
		{
			controller.StartDialogue(data);
			Assert.AreEqual(controller.CurrentQuote, data.quotes[0]);
			controller.Skip();
			Assert.AreEqual(controller.CurrentQuote, data.quotes[1]);
			Assert.True(data.quotes.Count == 2);
			
			var endEventCalled = false;
			controller.OnDialogueEnded += MarkEventCalled;
			controller.Skip();
			controller.OnDialogueEnded -= MarkEventCalled;
			
			Assert.That(endEventCalled);
			Assert.That(controller.CurrentDialogue == null);

			void MarkEventCalled() => endEventCalled = true;
		}

		private static DialogueData GetTestData()
		{
			var newData = ScriptableObject.CreateInstance<DialogueData>();
			newData.quotes = new List<Quote>
			{
				new Quote
				{
					talker = Talker.Npc,
					text = "testQuote",
					title = "testTitle"
				},
				new Quote
				{
					talker = Talker.Player,
					text = "testQuote2",
					title = "testTitle2"
				},
			};

			newData.connections = new List<Connection> {new Connection(0, 1)};
			newData.entryQuote = newData.quotes[0];
			return newData;
		}
	}
}