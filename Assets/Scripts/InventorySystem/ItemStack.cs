using System;

namespace InventorySystem
{
    public class ItemStack
    {
        public event Action OnChanged;
        
        private const int ItemMaxStack = 10;
        public ItemData data;

        private int _count;
        public int count
        {
            get => _count;
            private set
            {
                _count = value;
                OnChanged?.Invoke();
            }
        }

        public ItemStack(ItemData data)
        {
            this.data = data;
            this.count = 1;
        }

        public bool TryIncreaseCount()
        {
            if (count >= ItemMaxStack) return false;
            count++;
            return true;
        }

        public bool TryDecreaseCount(int val)
        {
            if (count == val) return false;
            count -= val;
            return true;
        }
    }
}