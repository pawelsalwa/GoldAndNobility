using System;

namespace InventorySystem
{
    public interface IInventory
    {
        event Action<int, Item> OnChangedAt;
        Item [] Items { get;}
        bool TryAddItem(ItemData data);
    }
}