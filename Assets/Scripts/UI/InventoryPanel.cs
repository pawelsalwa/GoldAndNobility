using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Const;
using GameManagement;
using NaughtyAttributes;
using RuntimeData;
using UnityEngine;

namespace UI
{
    internal class InventoryPanel : UiPanelBase
    {
        private IInventory service;

        public List<ItemIcon> icons = new List<ItemIcon>();
        private IInventoryManager manager;

        protected override void Start()
        {
            service = ServiceLocator.RequestService<IInventory>();
            service.OnChangedAt += UpdateItemAt;
            
            manager = ServiceLocator.RequestService<IInventoryManager>();
            manager.OnInventoryOpened += Open;
            manager.OnInventoryClosed += Close;

            GameState.OnChanged += OnStateChanged;
        }

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