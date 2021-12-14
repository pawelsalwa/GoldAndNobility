using System;

namespace InventorySystem
{
    public class Gold : IGold
    {
        private int _gold;
        public event Action<int> OnGoldChanged;

        public int amount
        {
            get => _gold;
            set
            {
                if (value < 0) throw new Exception("Cant set gold below 0");
                _gold = value;
                OnGoldChanged?.Invoke(value);
            }
        }
    }
}