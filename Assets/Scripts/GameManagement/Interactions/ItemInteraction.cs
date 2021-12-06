using Common;
using InteractionSystem;
using RuntimeData;
using UnityEngine.Serialization;

namespace GameManagement.Interactions
{
    public class ItemInteraction : Interactable
    {

        [FormerlySerializedAs("item")] public ItemData itemData;
        private IInventory service;

        public override string InteractionText => itemData.name;

        private void Start() => service = ServiceLocator.RequestService<IInventory>();

        protected override void OnInteraction()
        {
            AddToInventory();
            Destroy(gameObject);
        }

        private void AddToInventory() => service.TryAddItem(itemData);
    }
}
