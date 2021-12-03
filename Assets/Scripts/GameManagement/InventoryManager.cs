using System;
using Common;
using Common.Attributes;
using GameInput;
using UnityEngine;

namespace GameManagement
{
    [PersistentComponent(typeof(IInventoryManager))]
    internal class InventoryManager : MonoBehaviour, IInventoryManager
    {
        public event Action OnInventoryOpened;
        public event Action OnInventoryClosed;

        private bool inventoryOpened;
        
        private void Start() => GameState.OnChanged += OnStateChanged;
        private void OnDestroy() => GameState.OnChanged -= OnStateChanged;

        private void OnStateChanged(GameStateType obj) => inventoryOpened = obj == GameStateType.BrowsingInventory;

        private void Update()
        {
            if (inventoryOpened) CheckInventoryClosing();
            else CheckInventoryOpening();
        }

        private void CheckInventoryClosing()
        {
            if (UiInput.closeInventory) CloseInventory();
        }
        
        private void CheckInventoryOpening()
        {
            if (GameplayInput.openInventory) OpenInventory();
        }

        private void OpenInventory()
        {
            GameState.ChangeState(GameStateType.BrowsingInventory);
            OnInventoryOpened?.Invoke();
        }

        private void CloseInventory()
        {
            GameState.CancelState(GameStateType.BrowsingInventory);
            OnInventoryClosed?.Invoke();
        }
    }

    public interface IInventoryManager
    {
        event Action OnInventoryOpened;
        event Action OnInventoryClosed;
    }
}