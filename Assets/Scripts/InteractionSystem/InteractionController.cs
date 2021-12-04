using System;
using Common;
using Common.Attributes;
using UnityEngine;

namespace InteractionSystem
{
    [GameService(typeof(IInteractionController), typeof(IInteractionFocusChanger))]
    internal class InteractionController : MonoBehaviour, IInteractionController, IInteractionFocusChanger
    {
        public event Action<Interactable> OnInteractableFocused;

        private Interactable current = null;

        private IInteractablesProvider provider;

        public bool TryInteract()
        {
            if (!enabled || !current) return false;
            current.Interact();
            return true;
        }

        public void EnableInteraction() => enabled = true;

        public void DisableInteraction() => enabled = false; // important thing is it saves performance also with disabling Update()

        private void Update()
        {
            var closest = GetInteractableClosestToCameraRay();
            if (closest != current) SwitchFocusTo(closest);
        }

        private void SwitchFocusTo(Interactable closest)
        {
            if (closest) closest.OnDestroyed += CheckOnDestroyed;
            OnInteractableFocused?.Invoke(current = closest);

            // this is kinda workaround, this manages pickupable items, and them being destroyed
            void CheckOnDestroyed()
            {
                closest.OnDestroyed -= CheckOnDestroyed;
                SwitchFocusTo(null);
            }
        }


        private Interactable GetInteractableClosestToCameraRay()
        {
            provider = ServiceLocator.RequestService<IInteractablesProvider>(); // makes sense to do in on update since with scene reload it could get destroyed
            if (provider == null) return null; // makes sense to nullcheck since this service is scene based!

            Interactable closest = null;
            var closestDist = float.MaxValue;

            for (var i = 0; i < provider.Interactables.Count; i++)
                closestDist = CheckClosestDist(i);

            return closest;

            float CheckClosestDist(int i)
            {
                var interactable = provider.Interactables[i];
                var dist = GetDistanceFromCameraRay(interactable.transform.position);
                if (dist > closestDist) return closestDist;
                closest = interactable;
                return closestDist = dist;
            }
        }

        /// <summary> Distance from point to Camera.forward vector (or line) </summary>
        private float GetDistanceFromCameraRay(Vector3 point)
        {
            var ray = new Ray(provider.CameraTransform.position, provider.CameraTransform.forward);
            float distance = Vector3.Cross(ray.direction, point - ray.origin).magnitude;
            return distance;
        }
    }
}