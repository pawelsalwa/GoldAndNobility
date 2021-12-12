using System;
using UnityEngine;

namespace InteractionSystem
{
	[RequireComponent(typeof(RectTransform))]
	public class InteractionHudPrompt : MonoBehaviour
	{

		// // [Tooltip("This is visible part that moves properly and is toggled on and off.")]
		// private TextMeshProUGUI interactionText;
		public event Action<string> OnTextChanged;
		public string currentText;
		
		private Camera cam;
		private IInteractionFocusChanger service;
		private RectTransform rectTransform;
		private InteractableBase current;
		private Canvas canvas;
		private RectTransform canvasRectTransform;

		private void Start()
		{
			service = InteractionSystem.focusChanger;
			service.OnInteractableFocused += OnFocusChanged;
			
			canvas = GetComponentInParent<Canvas>();
			canvasRectTransform = canvas.GetComponent<RectTransform>();
			
			cam = Camera.main;
			rectTransform = transform as RectTransform;
			gameObject.SetActive(false);
		}

		private void OnDestroy() => service.OnInteractableFocused -= OnFocusChanged;

		private void OnFocusChanged(InteractableBase obj)
		{
			if (obj) OnFocused(obj);
			else OnFocusLost();
		}

		private void OnFocusLost()
		{
			current = null;
			gameObject.SetActive(false);
		}

		private void OnFocused(InteractableBase obj)
		{
			current = obj;
			currentText = obj.InteractionText;
			OnTextChanged?.Invoke(obj.InteractionText);
			gameObject.SetActive(true);
		}

		private void Update() => UpdatePromptPos(); // this object gets disabled so no update is when theres no interactable :)

		private void UpdatePromptPos()
		{
			// if (!current) return; not needed as we turn off whole object and it wont be updated if no interactalbe is current ;d

			//then you calculate the position of the UI element
			//0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

			var viewportPosition = cam.WorldToViewportPoint(current.TextPosition);
			var worldObjectScreenPosition = new Vector2(
				viewportPosition.x * canvasRectTransform.sizeDelta.x - canvasRectTransform.sizeDelta.x * 0.5f,
				viewportPosition.y * canvasRectTransform.sizeDelta.y - canvasRectTransform.sizeDelta.y * 0.5f);

			rectTransform.anchoredPosition = worldObjectScreenPosition;
		}
	}
}