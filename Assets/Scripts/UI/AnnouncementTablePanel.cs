using Common;
using GameManagement;
using UnityEngine.UI;

namespace UI
{
    internal class AnnouncementTablePanel : UiPanelBase
    {
        private AnnouncementTableManager service;
        public Button closeBtn;

        protected override void Start()
        {
            base.Start();
            service = ServiceLocator.RequestService<AnnouncementTableManager>();
            service.OnTableOpened += Open;
            service.OnTableClosed += Close;
            closeBtn.onClick.AddListener(service.HideTable);
        }

        protected override void OnDestroy()
        {
            service.OnTableOpened -= Open;
            service.OnTableClosed -= Close;
            closeBtn.onClick.RemoveListener(service.HideTable);
        }
    }
}