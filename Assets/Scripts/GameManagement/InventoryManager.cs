using System;
using Common;
using Common.Attributes;
using GameInput;
using UnityEngine;

namespace GameManagement
{
    [GameService(typeof(IInventoryManager))]
    internal class InventoryManager : MonoBehaviour, IInventoryManager
    {
        public event Action OnInventoryOpened;
        public event Action OnInventoryClosed;

        private void Update()
        {
            if (GameState.Current == GameStateType.InGame && GameplayInput.openInventory) OpenInventory();
            else if (GameState.Current == GameStateType.BrowsingInventory && UiInput.closeInventory) CloseInventory();
        }

        public void OpenInventory()
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

        void OpenInventory();
    }
}