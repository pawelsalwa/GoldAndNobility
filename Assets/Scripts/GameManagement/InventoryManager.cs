using System;
using Common;
using Common.Attributes;
using GameInput;
using UnityEngine;

namespace GameManagement
{
    [PersistentComponent()]
    internal class InventoryManager : MonoBehaviour, IInventoryManager
    {
        public event Action OnInventoryOpened;
        public event Action OnInventoryClosed;
        
        private void Update()
        {
            // if (GameState.Current == GameStateType.InventoryOpened) CheckInventoryClosing();
            // if (GameState.Current == GameStateType.InGame) CheckInventoryOpening();
            
        }

        // private void CheckInventoryClosing()
        // {
        //     if (GameplayInput.openInventory) CloseInventory();
        // }
        //
        // private void CheckInventoryOpening()
        // {
        //     if (GameplayInput.openInventory) OpenInventory();
        // }

        // private void OpenInventory() => GameState.Current = GameStateType.InventoryOpened;
        //
        // private void CloseInventory() => GameState.Current = GameStateType.InGame;
    }

    internal interface IInventoryManager
    {
        event Action OnInventoryOpened;
        event Action OnInventoryClosed;
    }
}