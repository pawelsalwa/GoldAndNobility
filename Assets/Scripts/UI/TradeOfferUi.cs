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
        public OfferSuccessInfo offerSuccessInfo;
        private ITradeManager service;
        private TradeOffer currentOffer;

        private void Awake()
        {
            service = ServiceLocator.RequestService<ITradeManager>();
            service.OnOfferCreated += ShowOffer;
            service.OnOfferAccepted += OnAcceptOffer;
            buyBtn.onClick.AddListener(AcceptOffer);
            maxBtn.onClick.AddListener(Maximize);
            priceSlider.onValueChanged.AddListener(OnSliderChanged);
            gameObject.SetActive(false);
        }

        private void OnAcceptOffer(TradeOffer obj)
        {
            gameObject.SetActive(false);
            offerSuccessInfo.Show(obj);
        }

        private void Maximize() => priceSlider.value = priceSlider.maxValue;

        private void AcceptOffer() => service.AcceptCurrentOffer();

        private void OnSliderChanged(float arg0)
        {
            currentOffer.itemsSellCount = (int) arg0;
            goldTxt.text = currentOffer.itemsSellCount.ToString();
        }

        private void OnDestroy()
        {
            service.OnOfferCreated -= ShowOffer;
            service.OnOfferAccepted -= OnAcceptOffer;
            buyBtn.onClick.RemoveListener(AcceptOffer);
            maxBtn.onClick.RemoveListener(Maximize);
            priceSlider.onValueChanged.RemoveListener(OnSliderChanged);
        }

        private void ShowOffer(TradeOffer obj)
        {
            gameObject.SetActive(true);
            currentOffer = obj;
            leftItemIcon.SetItem(obj.itemToSell);
            priceSlider.minValue = 0;
            priceSlider.value = 1;
            priceSlider.maxValue = obj.maxItemsSellCount;
        }
    }
}