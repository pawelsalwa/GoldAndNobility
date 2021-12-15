using System;

namespace InventorySystem
{
    public interface IInventory
    {
        event Action<int, ItemStack> OnChangedAt;
        ItemStack [] Items { get;}
        bool TryAddItem(ItemData data, int count = 1);
        void RemoveItems(ItemStack item, int count);
    }
}