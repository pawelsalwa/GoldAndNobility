using Common;
using GameManagement;
using InventorySystem;

namespace UI
{
    internal class InventoryPanel : UiPanelBase
    {
        
        private IInventoryManager manager;
        public InventoryUi inventoryUi;
        
        protected override void Start()
        {
            base.Start();
            manager = ServiceLocator.RequestService<IInventoryManager>();
            manager.OnInventoryOpened += Open;
            manager.OnInventoryClosed += Close;
            
            inventoryUi.Init(ServiceLocator.RequestService<IPlayerCharacterSingleton>().tradeEntity.inventory);
        }
        
        protected override void OnDestroy()
        {
            manager.OnInventoryOpened -= Open;
            manager.OnInventoryClosed -= Close;
        }
    }
}