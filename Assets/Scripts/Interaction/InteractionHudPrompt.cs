using Common;
using UnityEngine;

namespace Interaction
{
	internal class InteractionHudPrompt : MonoBehaviour
	{

		[Tooltip("This is visible part that moves properly and is toggled on and off.")]
		public RectTransform uiPrompt;
		private Camera cam;
		private IInteractionController service;

		private void Awake()
		{
			cam = Camera.main;
			service = ServiceLocator.RequestService<IInteractionController>();
			uiPrompt.gameObject.SetActive(false);
		}

		private void Update()
		{
			uiPrompt.gameObject.SetActive(service.Current);
			if (service.Current) UpdatePromptPos();
		}

		private void UpdatePromptPos()
		{
			var canvas = GetComponentInParent<Canvas>();
			var rectTransform = canvas.GetComponent<RectTransform>();

			//then you calculate the position of the UI element
			//0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

			var viewportPosition = cam.WorldToViewportPoint(service.Current.transform.position);
			var worldObjectScreenPosition = new Vector2(
				viewportPosition.x * rectTransform.sizeDelta.x - rectTransform.sizeDelta.x * 0.5f,
				viewportPosition.y * rectTransform.sizeDelta.y - rectTransform.sizeDelta.y * 0.5f);

			uiPrompt.anchoredPosition = worldObjectScreenPosition;
		}
	}
}