using System;
using System.Collections.Generic;
using InventorySystem;
using NaughtyAttributes;
using UnityEngine;

namespace GameManagement
{
    public class TradeEntity : MonoBehaviour
    {
        public readonly IInventory inventory = new Inventory();
        // public List<ItemData> demands;
        public List<ItemStartSetup> initialItems;
        public ItemData itemToAdd;

        private void Awake()
        {
            InitializeInitialItems();
        }

        private void InitializeInitialItems()
        {
            foreach (var setup in initialItems)
            {
                for (int i = 0; i < setup.count; i++)
                {
                    var succ = inventory.TryAddItem(setup.itemToAdd);
                    if (!succ) Debug.Log($"<color=red>[TradeEntity] setup not proper, failed to add item to inventory, probably too much items in initial setup</color>");
                }
            }
        }

        [Button] private void AddItem() => inventory.TryAddItem(itemToAdd);

        [Serializable]
        public class ItemStartSetup
        {
            public ItemData itemToAdd;
            public int count = 1;
        }
    }
}