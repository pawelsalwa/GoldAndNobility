using System;

namespace InventorySystem
{
    public interface IInventory
    {
        event Action<int, ItemStack> OnChangedAt;
        ItemStack [] Items { get;}
        bool TryAddItem(ItemData data);
        void RemoveItems(ItemStack item, int count);
    }
}