using System;
using System.Collections.Generic;
using InventorySystem;
using NaughtyAttributes;
using UnityEngine;

namespace TradeSystem
{
    public class TradeEntity : MonoBehaviour
    {
        public readonly IInventory inventory = new Inventory();
        public readonly IGold gold = new Gold();
        
        // public List<DemandSetup> demands;
        public List<ItemStartSetup> initialItems;
        public ItemData itemToAdd;
        public float priceValueMultiplier = 1f;

        private void Awake()
        {
            InitializeInitialItems();
            gold.amount += 150;
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

        public int HowMuchGoldICanPayFor(ItemStack item)
        {
            //todo advanced system for pricing items :)
            return (int) (item.data.defaultPrice * priceValueMultiplier);
        }

        public int HowMuchGoldIWantFor(ItemStack item)
        {
            //todo advanced system for pricing items :)
            return (int) (item.data.defaultPrice * priceValueMultiplier);
        }
    }
}