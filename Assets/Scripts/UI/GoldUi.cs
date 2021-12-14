using Common;
using GameManagement;
using InventorySystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GoldUi : MonoBehaviour
    {
        private IGold gold;
        public TextMeshProUGUI text;

        private void Start()
        {
            gold = ServiceLocator.RequestService<IPlayerCharacterSingleton>().tradeEntity.gold;
            gold.OnGoldChanged += UpdateUi;
            UpdateUi(gold.amount);
        }

        private void OnDestroy()
        {
            gold.OnGoldChanged -= UpdateUi;
        }

        private void UpdateUi(int obj) => text.text = obj.ToString();
    }
}