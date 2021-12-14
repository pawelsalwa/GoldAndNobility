using Common;
using InteractionSystem;
using UnityEngine.Serialization;
using InventorySystem;
using UnityEngine;

namespace GameManagement.Interactions
{
    public class ItemInteraction : InteractableBase
    {

        [FormerlySerializedAs("item")] public ItemData itemData;
        private IInventory playerInventory;

        public override string InteractionText => itemData.name;

        private void Start()
        {
            playerInventory = ServiceLocator.RequestService<IPlayerCharacterSingleton>().tradeEntity.inventory;
        }

        protected override void OnInteraction()
        {
            AddToInventory();
            Destroy(gameObject);
        }

        private void AddToInventory() => playerInventory.TryAddItem(itemData);
    }
}
