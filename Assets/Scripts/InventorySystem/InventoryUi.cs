using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem 
{
    public class InventoryUi : UIBehaviour
    {
        internal event Action<ItemIcon, bool> OnItemInspected;
        public event Action<ItemStack> OnItemSelected;
        
        private IInventory current;

        [SerializeField] private List<ItemIcon> icons = new();

        public void Init(IInventory inventory)
        {
            if (current != null) current.OnChangedAt -= UpdateItemAt; // cleanup memory if inited multiple times :)

            current = inventory;
            inventory.OnChangedAt += UpdateItemAt;
            for (var i = 0; i < inventory.Items.Length; i++)
                if (inventory.Items[i] != null)
                    UpdateItemAt(i, inventory.Items[i]);
        }

        protected override void Start()
        {
            foreach (var icon in icons)
            {
                icon.OnMouseEntered += OnMouseOverItem;
                icon.OnClick += OnItemClicked;
            }
        }

        protected override void OnDestroy()
        {
            foreach (var icon in icons)
            {
                icon.OnMouseEntered -= OnMouseOverItem;
                icon.OnClick -= OnItemClicked;
            }
        }

        private void OnItemClicked(ItemStack item) => OnItemSelected?.Invoke(item);

        private void OnMouseOverItem(object sender, EventArgs e) => OnItemInspected?.Invoke((ItemIcon)sender, ((ItemFocusEventArgs) e).mouseEntered);

        private void UpdateItemAt(int idx, ItemStack obj) => icons[idx].SetItem(obj);

        // protected override void OnValidate()
        // {
        //     if (icons.Count > GameConsts.InventorySlotsCount)
        //     {
        //         Debug.Log($"<color=white>Inventory Ui cant have more icons than specified in: <b>GameConsts.InventorySlotsCount</b> </color>");
        //         icons.RemoveRange(GameConsts.InventorySlotsCount, icons.Count - 1);
        //     }
        //
        //     if (icons.Count < GameConsts.InventorySlotsCount)
        //     {
        //         Debug.Log($"<color=orange>Not sufficient icons in inventory ui!</color>", gameObject);
        //     }
        // }

        [ContextMenu("GatherChildrenIcons()")]
        private void GatherChildrenIcons() => icons = GetComponentsInChildren<ItemIcon>().ToList();
    }
}