using System;
using Common;
using GameManagement;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal class TradeOfferUi : MonoBehaviour
    {

        public ItemIcon leftItemIcon;
        public Button maxBtn;
        public Button buyBtn;
        public Slider priceSlider;
        public TextMeshProUGUI goldTxt;
        private ITradeManager service;
        private TradeOffer currentOffer;
        private int quantity;

        private void Awake()
        {
            service = ServiceLocator.RequestService<ITradeManager>();
            service.OnOfferCreated += ShowOffer;
            buyBtn.onClick.AddListener(AcceptOffer);
            maxBtn.onClick.AddListener(Maximize);
            priceSlider.onValueChanged.AddListener(OnSliderChanged);
        }

        private void Maximize() => priceSlider.value = priceSlider.maxValue;

        private void AcceptOffer()
        {
            service.AcceptCurrentOffer(quantity);
        }

        private void OnSliderChanged(float arg0)
        {
            quantity = (int) arg0;
            goldTxt.text = quantity.ToString();
        }

        private void OnDestroy()
        {
            service.OnOfferCreated -= ShowOffer;
            buyBtn.onClick.RemoveListener(AcceptOffer);
            maxBtn.onClick.RemoveListener(Maximize);
            priceSlider.onValueChanged.RemoveListener(OnSliderChanged);
        }

        private void ShowOffer(TradeOffer obj)
        {
            currentOffer = obj;
            leftItemIcon.SetItem(obj.itemToSell);
            priceSlider.minValue = 0;
            priceSlider.maxValue = obj.maxItemsSellCount;
        }
    }
}