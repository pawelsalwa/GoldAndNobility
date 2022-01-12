using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem
{
	[CreateAssetMenu(fileName = "NewDialogue", menuName = "ScriptableObject/DialogueData")]
	public class DialogueData : ScriptableObject
	{
		public List<Quote> quotes = new();
		public List<Connection> connections = new();
		
		[field: SerializeField] private int entryQuoteIdx;
		
		public Quote entryQuote
		{
			get => quotes.Count == 0 ? null : quotes[entryQuoteIdx];
			set => entryQuoteIdx = quotes.IndexOf(value);
		}
		
		public void AddEdge(Quote @out, Quote @in)
		{
			var con = new Connection(quotes.IndexOf(@out), quotes.IndexOf(@in));
			connections.Add(con);
		}

		public void RemoveEdge(Quote @out, Quote @in)
		{
			var edge = connections.FirstOrDefault(IsSuitableEdge);
			connections.Remove(edge);

			bool IsSuitableEdge(Connection c) =>
				c.inputIdx == quotes.IndexOf(@in) &&
				c.outputIdx == quotes.IndexOf(@out);
		}

		public void AddQuote(Quote q) => quotes.Add(q);

		public List<Quote> GetNextQuotesFor(Quote quote)
		{
			if (!quotes.Contains(quote)) throw new ArgumentException("Trying to get next quotes for quote not contained within this dialogue.");
			var quoteIdx = quotes.IndexOf(quote);
			var outputConnections = connections.Where(c => c.outputIdx == quoteIdx);
			var outputQuotes = outputConnections.Select(con => quotes[con.inputIdx]);
			return outputQuotes.ToList();
		}
	}
}