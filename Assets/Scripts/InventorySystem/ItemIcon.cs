using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
    internal class ItemIcon : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event EventHandler OnMouseEntered;
        public event Action<Item> OnClick;
        private readonly ItemFocusEventArgs args = new(false);

        public Image image;
        public Item currentItem;
        public TextMeshProUGUI countTxt;

        public void SetItem(Item item)
        {
            currentItem = item;
            if (item == null)
            {
                image.sprite = null;
                image.enabled = false;
                countTxt.text = "";
            }
            else
            {
                image.sprite = item.data.sprite;
                image.enabled = true;
                if (item.data.stacks) countTxt.text = item.Count.ToString();
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (!image) image = GetComponent<Image>();
        }
#endif
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (currentItem == null) return;
            args.mouseEntered = true;
            OnMouseEntered?.Invoke(this, args);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (currentItem == null) return;
            args.mouseEntered = false;
            OnMouseEntered?.Invoke(this, args);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (currentItem == null) return;
            OnClick?.Invoke(currentItem);
        }
    }


    public class ItemFocusEventArgs : EventArgs
    {
        public bool mouseEntered;
        public ItemFocusEventArgs(bool mouseEntered) => this.mouseEntered = mouseEntered;
    }
}