using System;
using Common;

namespace UI
{
    internal class InventoryPanel : UiPanelBase
    {
        protected override void Awake()
        {
            base.Awake();
            // GameState.OnChanged += OpenOnInventoryState;
        }
    }
}