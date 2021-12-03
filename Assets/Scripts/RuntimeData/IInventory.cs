using System;

namespace RuntimeData
{
    public interface IInventory
    {
        event Action<int, Item> OnChangedAt;
        void TryAddItem(Item item);
    }
}