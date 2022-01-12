using System;
using UnityEngine;

namespace InteractionSystem
{
    /// <summary> this component starts disabled, and will enable when initialized with detector by scene </summary>
    public class InteractionController : MonoBehaviour, IInteractionController, IInteractionFocusChanger
    {
        public event Action<InteractableBase> OnInteractableFocused;

        private InteractableBase current = null;

        private IInteractablesProvider provider;

        void IInteractionController.Init(IInteractablesProvider sceneObject)
        {
            provider = sceneObject;
            EnableInteraction();
        }

        private void Awake()
        {
            InteractionSystem.controller = this;
            InteractionSystem.focusChanger = this;
            if (provider == null) DisableInteraction();
        }

        public bool TryInteract()
        {
            if (!enabled || !current) return false;
            current.Interact();
            return true;
        }

        public void EnableInteraction() => enabled = true;

        public void DisableInteraction() => enabled = false; // important thing is it saves performance also with disabling Update()

        private void OnDisable()
        {
            DisableInteraction();
            if (current) SwitchFocusTo(null);
        }

        private void Update()
        {
            var closest = GetInteractableClosestToCameraRay();
            if (closest != current) SwitchFocusTo(closest);
        }

        private void SwitchFocusTo(InteractableBase closest)
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


        private InteractableBase GetInteractableClosestToCameraRay()
        {
            // provider = ServiceLocator.RequestService<IInteractablesProvider>(); // makes sense to do in on update since with scene reload it could get destroyed
            if (provider == null) throw new Exception("if interactables provider gets destroyed, this controller should be disabled, call .DisableInteraction()");

            InteractableBase closest = null;
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