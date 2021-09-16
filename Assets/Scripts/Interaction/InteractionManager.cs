using Common;
using Common.Attributes;
using UnityEngine;

namespace Interaction
{
	[PersistentComponent]
	internal class InteractionManager : MonoBehaviour, IInteractionManager
	{

		public Interactable Current { get; private set; } = null;

		private Transform _cameraTransform;
		private Transform cameraTransform => _cameraTransform == null ? _cameraTransform = Camera.main.transform : _cameraTransform;
		
		private InteractionManager() => ServiceLocator.RegisterService<IInteractionManager>(this);

		private void Update() => Current = GetInteractableClosestToCameraRay();

		private Interactable GetInteractableClosestToCameraRay()
		{
			var service = ServiceLocator.RequestService<IInteractablesProvider>(); // makes sense to do in on update since with scene reload it could get destroyed
			if (service == null) return null; // makes sense to nullcheck since this service is scene based!

			Interactable closest = null;
			var closestDist = float.MaxValue;

			foreach (var interactable in service.Interactables)
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
			var ray = new Ray(cameraTransform.position, cameraTransform.forward);
			float distance = Vector3.Cross(ray.direction, point - ray.origin).magnitude;
			return distance;
		}

	}

}


