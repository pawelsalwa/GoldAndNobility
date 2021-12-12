using System;
using Common;
using Common.Attributes;
using GameInput;
using InventorySystem;
using UnityEngine;

namespace GameManagement
{
    [GameService(typeof(IInventoryManager))]
    internal class InventoryManager : MonoBehaviour, IInventoryManager
    {
        public event Action OnInventoryOpened;
        public event Action OnInventoryClosed;
        public IInventory PlayerInventory { get; } = new Inventory();

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

        /// <summary> i got no idea where to put it, should it be accesible through character service maybe? that would be similar to other characters i guess :\</summary>
        IInventory PlayerInventory { get; }
    }
}