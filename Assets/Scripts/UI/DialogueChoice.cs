using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class DialogueChoice : UIBehaviour
    {
        private Action OnChoiceClicked;
        
        [SerializeField] private TextMeshProUGUI text;
        private Button btn;

        public void Setup(string newText, Action clickedCallback)
        {
            gameObject.SetActive(true); // template should be inactive i guess :)
            text.text = newText;
            OnChoiceClicked = clickedCallback;
        }
        
        protected override void Start()
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(OnBtn);
        }

        protected override void OnDestroy()
        {
            btn.onClick.RemoveListener(OnBtn);
        }

        private void OnBtn() => OnChoiceClicked?.Invoke();
    }
}