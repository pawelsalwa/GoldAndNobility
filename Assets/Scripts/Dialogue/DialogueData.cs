using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialogue
{
	[CreateAssetMenu(fileName = "NewConversation", menuName = "ScriptableObject/DialogueData")]
	public class DialogueData : ScriptableObject
	{
		public List<Quote> quotes;
		public List<Connection> connections;

		public void AddEdge(Quote @out, Quote @in)
		{
			var con = new Connection(quotes.IndexOf(@out), quotes.IndexOf(@in));
			connections.Add(con);
		}

		public void RemoveEdge(Quote @in, Quote @out)
		{
			var edge = connections.FirstOrDefault(IsSuitableEdge);
			connections.Remove(edge);

			bool IsSuitableEdge(Connection c) =>
				c.inputIdx == quotes.IndexOf(@in) &&
				c.outputIdx == quotes.IndexOf(@out);
		}
	}
}