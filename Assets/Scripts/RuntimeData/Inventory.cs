using System;
using Common.Attributes;
using Common.Const;
using NaughtyAttributes;
using UnityEngine;

namespace RuntimeData
{
    [PersistentComponent(typeof(IInventory))]
    public class Inventory : MonoBehaviour, IInventory
    {
        
        public event Action<int, Item> OnChangedAt;

        [SerializeField, ReadOnly] public Item[] items = new Item[GameConsts.InventorySlotsCount];

        private void Awake()
        {
            items = new Item[GameConsts.InventorySlotsCount];
        }
        
        public void TryAddItem(Item item)
        {
            if (!TryGetFirstEmptyIdx(out int idx))
            {
                Debug.Log($"<color=white>no space in inventory</color>");
                return;
            }
            items[idx] = item;
            OnChangedAt?.Invoke(idx, item);
        }

        private bool TryGetFirstEmptyIdx(out int idx )
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i]) continue;
                idx = i;
                return true;
            }

            idx = -1;
            return false;
        }
    }
}
