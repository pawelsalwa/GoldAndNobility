namespace InventorySystem
{
    public class Item
    {
        private const int ItemMaxStack = 10;
        public ItemData data;
        private int count;
        public int Count => count;

        public Item(ItemData data)
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
        
    }
}