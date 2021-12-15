using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
    public class ItemIcon : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event EventHandler OnMouseEntered;
        public event Action<ItemStack> OnClick;
        private readonly ItemFocusEventArgs args = new(false);

        public Image image;
        public ItemStack currentItem;
        public TextMeshProUGUI countTxt;

        private static ItemIcon currentlySelected;

        public Color selectedColor;
        public Image selectedImg;

        public static void UnselectCurrent()
        {
            if (currentlySelected) currentlySelected.MarkSelected(false);
        }

        public void SetItem(ItemStack item)
        {
            if (currentItem != null) currentItem.OnChanged -= UpdateItemUi;
            currentItem = item;
            if (currentItem != null) currentItem.OnChanged += UpdateItemUi;
            UpdateItemUi();
        }

        private void UpdateItemUi()
        {
            if (currentItem == null)
            {
                UnselectCurrent();
                image.sprite = null;
                image.enabled = false;
                countTxt.text = "";
            }
            else
            {
                UnselectCurrent();
                image.sprite = currentItem.data.sprite;
                image.enabled = true;
                countTxt.text = currentItem.data.stacks ? currentItem.count.ToString() : "";
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
            if (currentlySelected) currentlySelected.MarkSelected(false);
            currentlySelected = this;
            MarkSelected(true);
            OnClick?.Invoke(currentItem);
        }

        private void MarkSelected(bool val)
        {
            selectedImg.color = val ? selectedColor : Color.white;
        }
    }


    public class ItemFocusEventArgs : EventArgs
    {
        public bool mouseEntered;
        public ItemFocusEventArgs(bool mouseEntered) => this.mouseEntered = mouseEntered;
    }
}