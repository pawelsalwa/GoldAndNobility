using Common;
using Interaction;
using NaughtyAttributes;
using UnityEngine;

namespace UI
{
	public class HudInteraction : UiPanelBase
	{
		protected override bool ShowOnAwake => true;

		public RectTransform uiPrompt;
		private Camera cam;
		private IInteractionManager service;

		protected override void Awake()
		{
			base.Awake();
			cam = Camera.main;
			service = ServiceLocator.RequestService<IInteractionManager>();
		}

		private void Update()
		{
			if (!Active) return;
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