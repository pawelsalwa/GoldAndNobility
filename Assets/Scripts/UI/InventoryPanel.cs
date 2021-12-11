using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using GameManagement;
using NaughtyAttributes;
using RuntimeData;

namespace UI
{
    internal class InventoryPanel : UiPanelBase
    {

        public event Action<ItemIcon, bool> OnItemInspected;
        public event Action<Item> OnClicked;
        
        private IInventory service;

        public List<ItemIcon> icons = new();

        private IInventoryManager manager;

        protected override void Start()
        {
            base.Start();
            service = ServiceLocator.RequestService<IInventory>();
            service.OnChangedAt += UpdateItemAt;
            
            manager = ServiceLocator.RequestService<IInventoryManager>();
            manager.OnInventoryOpened += Open;
            manager.OnInventoryClosed += Close;

            GameState.OnChanged += OnStateChanged;

            foreach (var icon in icons)
            {
                icon.OnMouseEntered += OnMouseOverItem;
                icon.OnClick += OnItemClicked;
            }
        }

        private void OnItemClicked(Item item) => OnClicked?.Invoke(item);

        private void OnMouseOverItem(object sender, EventArgs e) => OnItemInspected?.Invoke((ItemIcon)sender, ((ItemFocusEventArgs) e).mouseEntered);

        private void OnStateChanged(GameStateType obj)
        {
            if (obj == GameStateType.Trading) Open();
            else Close();
        }

        protected override void OnDestroy()
        {
            service.OnChangedAt -= UpdateItemAt;
            manager.OnInventoryOpened -= Open;
            manager.OnInventoryClosed -= Close;
            GameState.OnChanged -= OnStateChanged;
            foreach (var icon in icons)
            {
                icon.OnMouseEntered -= OnMouseOverItem;
                icon.OnClick -= OnItemClicked;
            }
        }

        private void UpdateItemAt(int idx, Item obj) => icons[idx].SetItem(obj);

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

        [Button()]
        private void GatherChildrenIcons() => icons = GetComponentsInChildren<ItemIcon>().ToList();
    }
}