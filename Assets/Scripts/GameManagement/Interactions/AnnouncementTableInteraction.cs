using Common;
using InteractionSystem;

namespace GameManagement.Interactions
{
    public class AnnouncementTableInteraction : InteractableBase
    {
        
        // public DialogueData DialogueData;
        private AnnouncementTableManager service;
        public override string InteractionText => "CheckAnnouncements";

        private void Start() => service = ServiceLocator.RequestService<AnnouncementTableManager>();

        protected override void OnInteraction() => ShowAnnouncementTable();

        private void ShowAnnouncementTable() => service.ShowTable();

    }
}