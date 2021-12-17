using System;
using Common;
using GameManagement;
using TMPro;
using UnityEngine;

namespace UI
{
    public class NobilityUi : MonoBehaviour
    {
        private Nobility nobility;
        public TextMeshProUGUI text;

        private void Start()
        {
            nobility = ServiceLocator.RequestService<IPlayerCharacterSingleton>().nobility;
            nobility.OnChanged += UpdateUi;
            UpdateUi(nobility.amount);
        }

        private void OnDestroy()
        {
            nobility.OnChanged -= UpdateUi;
        }

        private void UpdateUi(int obj)
        {
            text.text = obj.ToString();
        }
    }
}
