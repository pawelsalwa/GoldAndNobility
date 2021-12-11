using System.Collections.Generic;
using RuntimeData;
using UnityEngine;

namespace TradeSystem
{
    [CreateAssetMenu(menuName = "ScriptableObject/TradeEntity", fileName = "TradeEntity", order = 0)]
    public class TradeEntity : ScriptableObject
    {
        public List<ItemData> demands;
        public List<ItemData> offers;
    }
}