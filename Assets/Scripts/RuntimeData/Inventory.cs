using System;
using Common.Attributes;
using Common.Const;
using UnityEngine;

namespace RuntimeData
{
    [GameService(typeof(IInventory))]
    internal class Inventory : MonoBehaviour, IInventory
    {
        public event Action<int, Item> OnChangedAt;

        private readonly Item[] items = new Item[GameConsts.InventorySlotsCount];

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