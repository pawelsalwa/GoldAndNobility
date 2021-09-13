using System;
using Common;
using Common.Fsm;
using NaughtyAttributes;
using UnityEngine;

namespace Character
{
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(Animator))]
	public class Character : MonoBehaviour
	{
		public Movement Movement;
		public Camera vCam;
		// public Fsm Fsm;
		// [HideInInspector] public PlayerInput playerInput;
		[Expandable, SerializeField] private CharacterSetup setup;

		// public PawnAnimator Animator;
		public Refs Refs;
		private IGameStates gameState;

		// private Animator animatorComponent;
		// public Controller Controller { private get; set; }

		private void Awake() 
		{
			// animatorComponent = GetComponent<Animator>();
			Movement = new Movement(GetComponent<CharacterController>(), setup.Movement);
			gameState = ServiceLocator.RequestService<IGameStates>();
			// Animator = new PawnAnimator(animatorComponent, setup.Animator);
			// Fsm = new Fsm(this, setup.Fsm);
			enabled = false;
			gameState.InGame.OnEntered += Enable;
		}

		private void Start()
		{
		}

		private void OnDestroy()
		{
			gameState.InGame.OnEntered -= Enable;
		}

		private void Enable() => enabled = true;

		private void Update()
		{
			Vector2 inp = Vector2.zero;
			if (Input.GetKey(KeyCode.W)) inp.y = 1f;
			if (Input.GetKey(KeyCode.S)) inp.y = -1f;
			if (Input.GetKey(KeyCode.D)) inp.x = 1f;
			if (Input.GetKey(KeyCode.A)) inp.x = -1f;
			bool shift = Input.GetKey(KeyCode.LeftShift);
			// if (Input.GetKey(KeyCode.A)) inp.z = -1f;
			// if (Input.GetKey(KeyCode.D)) inp.z = 1f;
			Movement.MoveByInput(inp, shift);
			Movement.SetLookAtAngle(vCam.transform.rotation.eulerAngles.y);
		}
		
		private void FixedUpdate()
		{
			// Fsm?.FixedUpdate();
			Movement.FixedUpdate();
		}
		
		// private void OnAnimatorMove() => Movement.OnAnimatorMove(animatorComponent.deltaPosition);
	}
}