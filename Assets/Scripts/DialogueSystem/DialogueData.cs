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
		public List<Edge> edges = new();
		
		[field: SerializeField] private int entryQuoteIdx;
		
		public Quote entryQuote
		{
			get => quotes.Count == 0 ? null : quotes[entryQuoteIdx];
			set => entryQuoteIdx = quotes.IndexOf(value);
		}

		public IEnumerable<Quote> EndingQuotes => quotes.Where(q => edges.All(c => c.outputIdx != quotes.IndexOf(q)));

		public void AddEdge(Quote @out, Quote @in)
		{
			var con = new Edge(quotes.IndexOf(@out), quotes.IndexOf(@in));
			edges.Add(con);
		}

		public void RemoveEdge(Quote @out, Quote @in)
		{
			var edge = edges.FirstOrDefault(IsSuitableEdge);
			edges.Remove(edge);

			bool IsSuitableEdge(Edge c) => c.inputIdx == quotes.IndexOf(@in) && c.outputIdx == quotes.IndexOf(@out);
		}

		public void AddQuote(Quote q) => quotes.Add(q);
		public void RemoveQuote(Quote q)
		{
			edges.RemoveAll(IsConnectedToRemovedNode); // remember to remove unused connections!
			// and fix indexes of other edges
			var nodeIdx = quotes.IndexOf(q);
			foreach (var edge in edges)
			{
				if (edge.inputIdx >= nodeIdx) edge.inputIdx--;
				if (edge.outputIdx >= nodeIdx) edge.outputIdx--;
			}
			quotes.Remove(q);
			
			bool IsConnectedToRemovedNode(Edge x) => x.inputIdx == quotes.IndexOf(q) || x.outputIdx == quotes.IndexOf(q);
		}

		public List<Quote> GetNextQuotesFor(Quote quote)
		{
			if (!quotes.Contains(quote)) throw new ArgumentException("Trying to get next quotes for quote not contained within this dialogue.");
			var quoteIdx = quotes.IndexOf(quote);
			var outputConnections = edges.Where(c => c.outputIdx == quoteIdx);
			var outputQuotes = outputConnections.Select(con => quotes[con.inputIdx]);
			return outputQuotes.ToList();
		}
	}
}