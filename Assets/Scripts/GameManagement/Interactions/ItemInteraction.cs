using Common;
using InteractionSystem;
using RuntimeData;

namespace GameManagement.Interactions
{
    public class ItemInteraction : Interactable
    {

        public Item item;
        private IInventory service;

        public override string InteractionText => item.name;

        private void Start() => service = ServiceLocator.RequestService<IInventory>();

        protected override void OnInteraction()
        {
            AddToInventory();
            Destroy(gameObject);
        }

        private void AddToInventory() => service.TryAddItem(item);
    }
}
