using System;
using UnityEngine;

namespace QuestSystem
{
    [Serializable]
    public class Connection
    {
        public Connection(int outputIdx, int inputIdx)
        {
            this.outputIdx = outputIdx;
            this.inputIdx = inputIdx;
        }
        public int outputIdx;
        public int inputIdx; // input is index of node that has this edge as input!! could be misconcepted
    }
}
