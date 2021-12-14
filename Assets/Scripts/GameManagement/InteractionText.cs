using InteractionSystem;
using TMPro;
using UnityEngine;

namespace GameManagement
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    [RequireComponent(typeof(InteractionHudPrompt))]
    public class InteractionText : MonoBehaviour
    {
        private InteractionHudPrompt prompt;
        private TextMeshProUGUI txt;

        private void Start()
        {
            txt = GetComponent<TextMeshProUGUI>();
            prompt = GetComponent<InteractionHudPrompt>();
            prompt.OnTextChanged += UpdateText;
            UpdateText(prompt.currentText);
        } 

        private void OnDestroy() => prompt.OnTextChanged -= UpdateText;

        private void UpdateText(string obj) => txt.text = obj;
    }
}