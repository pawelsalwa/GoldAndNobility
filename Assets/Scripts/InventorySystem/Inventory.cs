using System;

namespace InventorySystem
{
    public class Inventory : IInventory
    {
        public const int InventorySlotsCount = 15;

        /// <summary> Called when item stack appears or is removed </summary>
        public event Action<int, ItemStack> OnChangedAt;

        private readonly ItemStack[] items = new ItemStack[InventorySlotsCount];

        public ItemStack[] Items => items;

        public bool TryAddItem(ItemData data, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                var succ = TryAddSingleItem(data);
                if (!succ) return false;
            }

            return true;
        }

        private bool TryAddSingleItem(ItemData data)
        {
            var success = false;
            if (TryAddToExistingStack(data, out var idx, out var item)) success = true;
            else if (TryCreateNewStack(data, out idx, out item)) success = true;

            // if (success) OnChangedAt?.Invoke(idx, item);
            return success;
        }

        public void RemoveItems(ItemStack target, int count)
        {
            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                if (item != null && item == target)
                {
                    if (item.count < count) throw new ArgumentException($"Trying to remove more items of type {target}, than there is in stack. For now by design we can only sell one stack of items at once (with maximum count constraint)");
                    if (!item.TryDecreaseCount(count))
                        items[i] = null;
                    OnChangedAt?.Invoke(i, items[i]);
                }
            }
        }

        private bool TryCreateNewStack(ItemData data, out int idx, out ItemStack item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null) continue;
                item = items[i] = new ItemStack(data);
                idx = i;
                OnChangedAt?.Invoke(idx, item);
                return true;
            }

            item = null;
            idx = -1;
            return false;
        }

        private bool TryAddToExistingStack(ItemData data, out int idx, out ItemStack item)
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