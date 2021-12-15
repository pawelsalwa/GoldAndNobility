using TMPro;
using UnityEngine;

namespace TradeSystem
{
    internal class OfferSuccessInfo : MonoBehaviour
    {

        public TextMeshProUGUI text;
        public GameObject shownObject;
        public float shownDuration = 3f;

        private void Awake() => Hide();

        public void Show(TradeOffer offer)
        {
            shownObject.SetActive(true);
            text.text = $"Sold {offer.itemsSellCount} {offer.itemToSell.data.name}{ (offer.itemsSellCount > 1 ? "s" : "")}, for {offer.totalGoldValue} gold.";
            CancelInvoke(nameof(Hide));
            Invoke(nameof(Hide), shownDuration);
        }

        private void Hide() => shownObject.SetActive(false);
    }
}