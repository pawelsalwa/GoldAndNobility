using System;
using Common;
using Common.Attributes;
using GameInput;
using InteractionSystem;
using UnityEngine;

namespace GameManagement
{
    [GameService()]
    internal class InteractionManager : MonoBehaviour
    {
        private IInteractionController service;

        private void Awake()
        {
            service = gameObject.AddComponent<InteractionController>();
            ServiceLocator.RegisterService(service);
        }

        private void Start()
        {
            GameState.OnChanged += OnGameStateChanged;
            OnGameStateChanged(GameState.Current);
        }

        private void OnDestroy() => GameState.OnChanged -= OnGameStateChanged;

        private void OnGameStateChanged(GameStateType obj)
        {
            if (obj == GameStateType.InGame) service.EnableInteraction();
            else service.DisableInteraction();
        }
        
        void Update()
        {
            if (GameplayInput.interact) service.TryInteract();
        }
    }
}
