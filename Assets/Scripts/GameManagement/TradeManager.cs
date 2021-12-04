using System;
using Common.Attributes;
using UnityEngine;

namespace GameManagement
{
    [GameService(typeof(ITradeManager))]
    public class TradeManager : MonoBehaviour, ITradeManager
    {
        
        public void ProceedTrade()
        {
        }

        public void FinishTrade()
        {
        }
    }

    public interface ITradeManager
    {
        void ProceedTrade();
    }
}