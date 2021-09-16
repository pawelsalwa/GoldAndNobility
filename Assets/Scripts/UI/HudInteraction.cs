using Common;
using Interaction;
using NaughtyAttributes;
using UnityEngine;

namespace UI
{
	public class HudInteraction : UiPanelBase
	{

		public Interactable current;

		protected override bool ShowOnAwake => true;
		
		public RectTransform uiPrompt;
		private Camera cam;

		protected override void Awake()
		{
			base.Awake();
			cam = Camera.main;
		}

		private void Update()
		{
			if (!Active) return;
			current = GetInteractableClosestToCameraRay();
			if (current)
			{
				uiPrompt.gameObject.SetActive(true);
				UpdatePromptPos();
			}
			else uiPrompt.gameObject.SetActive(false);
		}

		// private void TryGetCurrentInteractable()
		// {
		// 	// if (current) current.GetComponent<MeshRenderer>().material.color = Color.red;
		// 	// if (current) current.GetComponent<MeshRenderer>().material.color = Color.cyan;
		// }

		private void UpdatePromptPos()
		{
			var canvas = GetComponentInParent<Canvas>();
			var rectTransform = canvas.GetComponent<RectTransform>();

			//then you calculate the position of the UI element
			//0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

			var viewportPosition = cam.WorldToViewportPoint(current.transform.position);
			var worldObjectScreenPosition = new Vector2(
				viewportPosition.x * rectTransform.sizeDelta.x - rectTransform.sizeDelta.x * 0.5f,
				viewportPosition.y * rectTransform.sizeDelta.y - rectTransform.sizeDelta.y * 0.5f);

			uiPrompt.anchoredPosition = worldObjectScreenPosition;
		}

		private Interactable GetInteractableClosestToCameraRay()
		{
			var interactables = ServiceLocator.RequestService<IInteractablesProvider>().Interactables;
			
			Interactable closest = null;
			var closestDist = float.MaxValue;
			
			foreach (var interactable in interactables)
			{
				var dist = GetDistanceFromCameraRay(interactable.transform.position);
				if (!(dist < closestDist)) continue;
				closest = interactable;
				closestDist = dist;
			}
			return closest;
		}

		/// <summary> Distance from point to Camera.forward vector (or line) </summary>
		private float GetDistanceFromCameraRay(Vector3 point)
		{
			var camTtransform = cam.transform;
			Ray ray = new Ray(camTtransform.position, camTtransform.forward);
			float distance = Vector3.Cross(ray.direction, point - ray.origin).magnitude;
			return distance;
		}
	}
}