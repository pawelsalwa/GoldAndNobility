using System;
using Common;
using Common.Attributes;
using GameInput;
using InteractionSystem;
using UnityEngine;

namespace GameManagement
{
   [PersistentComponent()]
    public class InteractionManager : MonoBehaviour
    {
        private IInteractionController service;

        private void Start()
        {
            GameState.OnChanged += OnGameStateChanged;
            service = ServiceLocator.RequestService<IInteractionController>();
        }

        private void OnDestroy() => GameState.OnChanged -= OnGameStateChanged;

        private void OnGameStateChanged(GameStateType obj)
        {
            if (obj == GameStateType.InGame) service.EnableInteraction();
            else service.DisableInteraction();
        }


        // Update is called once per frame
        void Update()
        {
            if (GameplayInput.interact) service.TryInteract();
        }
    }
}
