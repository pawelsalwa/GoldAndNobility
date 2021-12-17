using System;
using UnityEngine;

namespace GameManagement
{
    [Serializable]
    public class Nobility 
    {
        public event Action<int> OnChanged;
        private int _amount;

        public int amount
        {
            get => _amount;
            set
            {
                if (value < 0) throw new Exception("Cant set gold below 0");
                _amount = value;
                OnChanged?.Invoke(value);
            }
        }
    }
}