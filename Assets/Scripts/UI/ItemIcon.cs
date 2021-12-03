using RuntimeData;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    internal class ItemIcon : UIBehaviour
    {
        public Image image;

        public void SetItem(Item item)
        {
            image.sprite = item ? item.sprite : null;
            image.enabled = item;
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (!image) image = GetComponent<Image>();
        }
#endif
    }
}