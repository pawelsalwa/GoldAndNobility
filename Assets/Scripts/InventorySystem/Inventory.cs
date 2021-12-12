using System;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : IInventory
    {
        public const int InventorySlotsCount = 15;
        
        public event Action<int, Item> OnChangedAt;

        private readonly Item[] items = new Item[InventorySlotsCount];
        public Item[] Items => items;

        public bool TryAddItem(ItemData data)
        {
            var success = false;
            if (TryAddToExistingStack(data, out var idx, out var item)) success = true;
            else if (TryCreateNewStack(data, out idx, out item)) success = true;

            if (success) OnChangedAt?.Invoke(idx, item);
            return success;
        }

        private bool TryCreateNewStack(ItemData data, out int idx, out Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null) continue;
                item = items[i] = new Item(data);
                idx = i;
                return true;
            }

            item = null;
            idx = -1;
            return false;
        }

        private bool TryAddToExistingStack(ItemData data, out int idx, out Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null || items[i].data != data) continue;
                if (!items[i].TryIncreaseCount()) continue;
                item = items[i];
                idx = i;
                return true;
            }

            item = null;
            idx = -1;
            return false;
        }
    }
}