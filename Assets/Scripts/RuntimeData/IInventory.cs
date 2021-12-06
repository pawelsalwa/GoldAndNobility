using System;

namespace RuntimeData
{
    public interface IInventory
    {
        event Action<int, Item> OnChangedAt;
        bool TryAddItem(ItemData data);
    }
}