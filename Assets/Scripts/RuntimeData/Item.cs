using Common.Const;

namespace RuntimeData
{
    public class Item
    {
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
            if (count >= GameConsts.ItemMaxStack) return false;
            count++;
            return true;
        }
        
    }
}