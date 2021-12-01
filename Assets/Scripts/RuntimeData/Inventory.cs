using System;
using System.Collections.Generic;
using Common.Attributes;
using UnityEngine;

namespace RuntimeData
{
    [PersistentComponent(typeof(IInventory))]
    public class Inventory : MonoBehaviour, IInventory
    {
        
        public event Action OnChanged;
        
        public List<Item> items = new List<Item>();


        public void TryAddItem(Item item)
        {
            items.Add(item);
            OnChanged?.Invoke();
        }
    }
}
