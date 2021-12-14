namespace InventorySystem
{
    public class ItemStack
    {
        private const int ItemMaxStack = 10;
        public ItemData data;
        private int count;
        public int Count => count;

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