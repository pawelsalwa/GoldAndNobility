using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    [RequireComponent(typeof(Button))]
    public class DialogueChoice : MonoBehaviour
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
        
        protected void Start()
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(OnBtn);
        }

        protected void OnDestroy()
        {
            btn.onClick.RemoveListener(OnBtn);
        }

        private void OnBtn() => OnChoiceClicked?.Invoke();
    }
}