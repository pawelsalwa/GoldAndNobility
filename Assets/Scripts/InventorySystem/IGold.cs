using System;

namespace InventorySystem
{
    public interface IGold
    {
        event Action<int> OnGoldChanged;
        int amount { get; set; }
    }
}