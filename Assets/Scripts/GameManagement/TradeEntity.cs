using InventorySystem;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(menuName = "ScriptableObject/TradeEntity", fileName = "TradeEntity", order = 0)]
    public class TradeEntity : ScriptableObject
    {
        public readonly IInventory inventory = new Inventory();
        // public List<ItemData> demands;
        // public List<ItemData> offers;
    }
}