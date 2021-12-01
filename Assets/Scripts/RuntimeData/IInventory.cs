using System;

namespace RuntimeData
{
    public interface IInventory
    {
        event Action OnChanged; 
        void TryAddItem(Item item);
    }
}