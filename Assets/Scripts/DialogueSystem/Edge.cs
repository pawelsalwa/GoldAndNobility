using System;

namespace DialogueSystem
{
	[Serializable]
	public class Edge
	{
		public Edge(int outputIdx, int inputIdx)
		{
			this.outputIdx = outputIdx;
			this.inputIdx = inputIdx;
		}
		public int outputIdx;
		public int inputIdx; // input is index of node that has this edge as input!! could be misconcepted
	}
}