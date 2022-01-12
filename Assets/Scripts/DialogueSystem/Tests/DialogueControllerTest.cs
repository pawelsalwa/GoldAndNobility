using System.Collections.Generic;
using DialogueSystem;
using NUnit.Framework;

namespace Tests
{
	internal class DialogueControllerTest
	{
		private DialogueController controller;
		private DialogueData data;

		[SetUp]
		public void Setup()
		{
			controller = DialogueController.GetTestInstance();
			data = TestData.GetTestDialogueData();
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
			controller.OnQuoteStarted -= OnQuoteStarted;
			Assert.That(saidQuote == data.quotes[0]);

			void OnQuoteStarted(Quote q) => saidQuote = q;
		}
		
		[Test]
		public void CheckEndEvent()
		{
			var saidQuotesCount = 4;
			Assert.True(data.quotes.Count == saidQuotesCount);
			var endEventCalled = false;
			controller.StartDialogue(data);
			controller.OnDialogueEnded += OnEndEvent;
			for (int i = 0; i < saidQuotesCount; i++) controller.Skip(); 
			controller.OnDialogueEnded -= OnEndEvent;
			Assert.That(endEventCalled); // to nie dziala bo trzeba wybierac quoty albo zmienic date zeby nie bylo choicow
			
			void OnEndEvent() => endEventCalled = true;
		}

		/// <summary> Dialogue data used should have choices right after first npc quote (after welcome) </summary>
		[Test]
		public void CheckPlayerChoices()
		{
			bool quotesShown = false;
			controller.StartDialogue(data);
			
			controller.OnPlayerQuotesAppear += CheckPlayerQuotes;
			controller.Skip();
			controller.OnPlayerQuotesAppear -= CheckPlayerQuotes;

			Assert.That(quotesShown);

			void CheckPlayerQuotes(List<Quote> quotes)
			{
				quotesShown = true;
				Assert.That(quotes[0] == data.quotes[1]); // shown quotes are from data
				Assert.That(quotes[1] == data.quotes[2]);
			}
		}
	}
}