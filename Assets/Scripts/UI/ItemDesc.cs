using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ItemDesc : UIBehaviour
    {

        [SerializeField] internal InventoryPanel inventory;
        public GameObject shownObj;
        public TextMeshProUGUI title;
        public TextMeshProUGUI desc;

        protected override void Start()
        {
            inventory.OnItemInspected += OnItemInspected;
            gameObject.SetActive(false);
        }

        protected override void OnDestroy()
        {
            inventory.OnItemInspected -= OnItemInspected;             
        }

        private void OnItemInspected(ItemIcon item, bool show)
        {
            if (show) ShowDesc(item);
            else HideDesc();
        }

        private void HideDesc()
        {
            shownObj.SetActive(false);
            desc.text = "";
            title.text = "";
        }

        private void ShowDesc(ItemIcon item)
        {
            shownObj.SetActive(true);
            desc.text = item.currentItem.data.desc;
            title.text = item.currentItem.data.name;
        }
    }
}