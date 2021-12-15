using System;
using GameManagement;
using TMPro;
using UnityEngine;

namespace UI
{
    internal class OfferSuccessInfo : UiPanelBase
    {

        public TextMeshProUGUI text;
        [SerializeField] private float shownDuration = 3f;

        protected override bool ShowOnAwake => false;

        public void Show(TradeOffer offer)
        {
            Open();
            text.text = $"Sold {offer.itemsSellCount} {offer.itemToSell.data.name}{ (offer.itemsSellCount > 1 ? "s" : "")}, for {offer.totalGoldValue} gold.";
            Invoke(nameof(Close), shownDuration);
        }
    }
}