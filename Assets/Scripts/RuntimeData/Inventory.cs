using System;
using Common.Attributes;
using Common.Const;
using NaughtyAttributes;
using UnityEngine;

namespace RuntimeData
{
    [GameService(typeof(IInventory))]
    internal class Inventory : MonoBehaviour, IInventory
    {
        public event Action<int, Item> OnChangedAt;

        private readonly Item[] items = new Item[GameConsts.InventorySlotsCount];

        public bool TryAddItem(ItemData data)
        {
            var success = false;
            if (TryAddToExistingStack(data, out var idx, out var item)) success = true;
            else if (TryCreateNewStack(data, out idx, out item)) success = true;
            
            OnChangedAt?.Invoke(idx, item);
            return success;
            
            // var shouldCreateNewStack = true;
            // if (TryGetExistingRuntimeItem(itemData, out var item))
            // {
            //     if (item.TryIncreaseCount())
            //     {
            //         OnChangedAt?.Invoke(, itemData);
            //         return true;
            //     }
            // }
            //
            // if (shouldCreateNewStack)
            // {
            //     if (TryGetFirstEmptyIdx(out int idx))
            //     {
            //         
            //         Debug.Log($"<color=white>no space in inventory</color>");
            //         return ;
            //     }
            // }
            //
            //
            // items[idx] = itemData;
            // OnChangedAt?.Invoke(idx, itemData);
        }

        private bool TryCreateNewStack(ItemData data, out int idx, out Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null) continue;
                item = items[i] = new Item(data);
                idx = i;
                return true;
            }

            item = null;
            idx = -1;
            return false;
        }

        private bool TryAddToExistingStack(ItemData data, out int idx, out Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null || items[i].data != data) continue;
                if (!items[i].TryIncreaseCount()) continue;
                item = items[i];
                idx = i;
                return true;
            }

            item = null;
            idx = -1;
            return false;
        }

        // private bool TryGetExistingRuntimeItem(ItemData data, out RuntimeItem item)
        // {
        //     for (int i = 0; i < items.Length; i++)
        //     {
        //         if (items[i].data == data)
        //         {
        //             item = items[i];
        //             return true;
        //         }
        //     }
        //
        //     item = null;
        //     return false;
        // }
        //
        // private bool TryGetFirstEmptyIdx(out int idx)
        // {
        //     for (int i = 0; i < items.Length; i++)
        //     {
        //         if (items[i]) continue;
        //         idx = i;
        //         return true;
        //     }
        //
        //     idx = -1;
        //     return false;
        // }
    }
}