using Common;
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
		[Expandable, SerializeField] internal CharacterSetup setup;
		public PlayerInput input;
		internal Animator animator;
		private AnimManager animManager;
		public AnimSetup animSetup;

		private void Awake()
		{
			Movement = new Movement(GetComponent<CharacterController>(), setup.Movement);
			animator = GetComponent<Animator>();
			animManager = new AnimManager(this);
		}

		private void Start()
		{
			PauseGameManager.OnPaused += Disable;
			PauseGameManager.OnResumed += Enable;
		}

		private void OnDestroy()
		{
			PauseGameManager.OnPaused -= Disable;
			PauseGameManager.OnResumed -= Enable;
		}

		private void Disable() => enabled = false;

		private void Enable() => enabled = true;

		private void Update()
		{ 
			Movement.Update(input);
			animManager.Update();
		}
		
		private void FixedUpdate()
		{
			// Fsm?.FixedUpdate();
			Movement.FixedUpdate();
		}
		
		// private void OnAnimatorMove() => Movement.OnAnimatorMove(animatorComponent.deltaPosition);
	}
}